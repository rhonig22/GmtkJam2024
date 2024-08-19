using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using static Unity.Collections.AllocatorManager;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _startingBlock;
    [SerializeField] AudioClip _moveSound;
    [SerializeField] AudioClip _blockedSound;
    [SerializeField] AudioClip _eatSound;
    private float _horizontalAxis = 0;
    private float _verticalAxis = 0;
    private readonly float _waitTime = .2f;
    private float _currentWait = 0;
    private float _moveSpeed = 1;
    private bool _eatPressed = false;
    private bool _moved = false;
    private LayerMask _blockMask;
    public static int Size { get; private set; } = 1;
    public static UnityEvent PlayerMoved = new UnityEvent();
    private List<GameObject> _blocksEaten = new List<GameObject>();

    private float _currentLensSize = 4.5f;
    private readonly float _lensStep = 2f;
    private float _currentPitch = 1;
    private readonly float _pitchStep = .05f;
    private int _currentPitchThreshold = 2;

    // Start is called before the first frame update
    void Start()
    {
        Size = 1;
        _blockMask = LayerMask.GetMask("Block");
        _blocksEaten.Add(_startingBlock);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Eat"))
            _eatPressed = true;

        _horizontalAxis = Input.GetAxisRaw("Horizontal");
        _verticalAxis = Input.GetAxisRaw("Vertical");
        _currentWait -= Time.deltaTime;
        if (_moved)
        {
            _moved = false;
            PlayerMoved.Invoke();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_eatPressed)
        {
            TryEat();
            _eatPressed=false;
        }

        if (_currentWait <= 0)
        {
            if (_horizontalAxis != 0)
            {
                Move(new Vector3(_moveSpeed * _horizontalAxis / Mathf.Abs(_horizontalAxis), 0));
                _currentWait = _waitTime;
                return;
            }

            if (_verticalAxis != 0)
            {
                Move(new Vector3(0, _moveSpeed * _verticalAxis / Mathf.Abs(_verticalAxis)));
                _currentWait = _waitTime;
            }
        }
    }

    private void Move(Vector3 moveVector)
    {
        if (IsBlocked(moveVector))
            Blocked();
        else
        {
            SoundManager.Instance.PlaySound(_moveSound, transform.position, _currentPitch);
            transform.Translate(moveVector);
            _moved = true;
        }
    }

    private bool IsBlocked(Vector3 moveVector)
    {
        var blocked = false;
        foreach (var block in _blocksEaten)
        {
            var hit = Physics2D.OverlapBox(block.transform.position + moveVector, block.transform.localScale, 0, _blockMask);
            blocked |= (hit != null);
        }

        return blocked;
    }

    private void Blocked()
    {
        SoundManager.Instance.PlaySound(_blockedSound, transform.position);
    }

    private void TryEat()
    {
        var blocksToEat = new List<GameObject>();
        foreach (var block in _blocksEaten)
        {
            for (int i = 0; i < 4; i++)
            {
                var x = i % 2;
                var y = (i + 1) % 2;
                if (i >= 2)
                {
                    x *= -1;
                    y *= -1;
                }

                var hit = Physics2D.OverlapBox(block.transform.position + new Vector3(x, y, 0) * _moveSpeed, block.transform.localScale, 0, _blockMask);
                if (hit != null && CanEat(hit.gameObject) && !blocksToEat.Contains(hit.gameObject))
                    blocksToEat.Add(hit.gameObject);
            }
        }

        if (blocksToEat.Count > 0)
            SoundManager.Instance.PlaySound(_eatSound, transform.position);
        else
            SoundManager.Instance.PlaySound(_blockedSound, transform.position);

        foreach (var block in blocksToEat)
        {
            block.transform.SetParent(transform, true);
            Size += GetBlockSize(block);
            _blocksEaten.Add(block);
            block.layer = 6;
            block.GetComponent<BlockController>().GetEaten();
        }

        if (Size >= _currentPitchThreshold)
        {
            _currentPitch -= _pitchStep;
            _currentPitchThreshold *= 2;
            _currentLensSize += _lensStep;
            CameraController.ChangeLensSize.Invoke(_currentLensSize);
        }
    }

    private bool CanEat(GameObject block)
    {
        var blockSize = GetBlockSize(block);
        return blockSize <= Size;
    }

    private int GetBlockSize(GameObject block)
    {
        return Mathf.FloorToInt(block.transform.localScale.x * block.transform.localScale.y);
    }
}

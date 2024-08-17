using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private int _blockSize = 1;
    private bool _isEaten = false;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _canEatColor;
    [SerializeField] private Color _cantEatColor;
    [SerializeField] private Color _playerColor;

    // Start is called before the first frame update
    void Start()
    {
        _blockSize = Mathf.FloorToInt(transform.localScale.x * transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isEaten)
        {
            if (_blockSize <= PlayerController.Size)
            {
                _spriteRenderer.color = _canEatColor;
            }
            else
            {
                _spriteRenderer.color = _cantEatColor;
            }
        }
    }

    public void GetEaten()
    {
        _isEaten = true;
        _spriteRenderer.color = _playerColor;
    }
}

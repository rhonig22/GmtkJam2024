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
    [SerializeField] private Sprite _squareSprite;
    [SerializeField] private Sprite _borderSprite;

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
                _spriteRenderer.sprite = _borderSprite;
                _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
                _spriteRenderer.color = _canEatColor;
                _spriteRenderer.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1);
                _spriteRenderer.size = new Vector2(transform.localScale.x, transform.localScale.y);
            }
            else
            {
                _spriteRenderer.sprite = _squareSprite;
                _spriteRenderer.drawMode = SpriteDrawMode.Simple;
                _spriteRenderer.color = _cantEatColor;
            }
        }
    }

    public void GetEaten()
    {
        _isEaten = true;
        _spriteRenderer.sprite = _squareSprite;
        _spriteRenderer.drawMode = SpriteDrawMode.Simple;
        _spriteRenderer.color = _playerColor;
        _spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
        _spriteRenderer.size.Set(1, 1);
    }
}

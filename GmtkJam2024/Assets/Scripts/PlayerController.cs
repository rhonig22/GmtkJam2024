using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _horizontalAxis = 0;
    private float _verticalAxis = 0;
    private readonly float _waitTime = .2f;
    private float _currentWait = 0;
    private float _moveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalAxis = Input.GetAxisRaw("Horizontal");
        _verticalAxis = Input.GetAxisRaw("Vertical");
        _currentWait -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_currentWait <= 0)
        {
            if (_horizontalAxis != 0)
            {
                transform.Translate(new Vector3(_moveSpeed * _horizontalAxis / Mathf.Abs(_horizontalAxis), 0));
                _currentWait = _waitTime;
                return;
            }

            if (_verticalAxis != 0)
            {
                transform.Translate(new Vector3(0, _moveSpeed * _verticalAxis / Mathf.Abs(_verticalAxis)));
                _currentWait = _waitTime;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] GameObject _lightSquare;
    [SerializeField] GameObject _darkSquare;
    [SerializeField] int _width;
    [SerializeField] int _height;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        var startLight = true;
        var iOffset = Mathf.FloorToInt(_width / 2);
        var jOffset = Mathf.FloorToInt(_height / 2);
        for (var i = 0; i < _width; i++)
        {
            var isLight = startLight;
            for (var j = 0; j < _height; j++)
            {
                var square = Instantiate(isLight ?  _lightSquare : _darkSquare, transform);
                square.transform.localPosition = new Vector3(i - iOffset, j - jOffset, 0);
                isLight = !isLight;
            }

            startLight = !startLight;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class ScaleController : MonoBehaviour
{
    [SerializeField] private int _size = 1;
    [SerializeField] private TextMeshProUGUI _weightText;
    [SerializeField] private TextMeshProUGUI _sizeText;
    private BoxCollider2D _thisCollider;
    private readonly int _defaultTextSize = 10;
    private bool _playerInBounds = false;
    private ContactFilter2D _playerFilter;

    // Start is called before the first frame update
    void Start()
    {
        _thisCollider = GetComponent<BoxCollider2D>();
        var side = Mathf.CeilToInt(Mathf.Sqrt(_size));
        if (side % 2 == 0)
            side += 1;

        transform.localScale = new Vector3(side, side, 0);
        _sizeText.text = "" + _size;
        _sizeText.fontSize = side * _defaultTextSize;
        _weightText.text = "0";
        _weightText.fontSize = side * _defaultTextSize;

        var playerMask = LayerMask.GetMask("Player");
        _playerFilter = new ContactFilter2D();
        _playerFilter.layerMask = playerMask;
        _playerFilter.useLayerMask = true;

        PlayerController.PlayerMoved.AddListener(() => CheckPlayerWeight());
    }

    private void CheckPlayerWeight()
    {
        List<Collider2D> results = new List<Collider2D>();
        _thisCollider.OverlapCollider(_playerFilter, results);
        int sumArea = 0;
        Debug.Log("checking " + results.Count);
        foreach (var hit in results)
        {
            sumArea += OverlapColliders((BoxCollider2D)hit, _thisCollider);
        }

        _weightText.text = "" + sumArea;
    }

    private int OverlapColliders(BoxCollider2D a, BoxCollider2D b)
    {
        var boundsA = a.bounds;
        var boundsB = b.bounds;

        if (!boundsA.Intersects(boundsB))
        {
            return 0;
        }

        var minA = boundsA.min;
        var maxA = boundsA.max;
        var minB = boundsB.min;
        var maxB = boundsB.max;

        var lowerMax = Vector3.Min(maxA, maxB);
        var higherMin = Vector3.Max(minA, minB);

        var overlappingBounds = lowerMax - higherMin;
        return Mathf.CeilToInt(overlappingBounds.x * overlappingBounds.y);
    }
}

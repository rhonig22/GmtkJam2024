using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioClip _levelFinishedSound;
    private List<ScaleController> _scaleControllers = new List<ScaleController>();
    private bool _isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        var scales = GameObject.FindGameObjectsWithTag("Scale");
        foreach (var scale in scales)
        {
            _scaleControllers.Add(scale.GetComponent<ScaleController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isComplete)
            return;

        var allComplete = true;
        foreach (var scale in _scaleControllers)
        {
            allComplete &= scale.ScaleComplete;
        }

        if (allComplete)
            LevelComplete();
    }

    private void LevelComplete()
    {
        _isComplete = true;
        var audio = SoundManager.Instance.PlaySound(_levelFinishedSound, transform.position);
        var length = audio.clip.length;
        StartCoroutine(ReturnToMenu(length));
    }

    private IEnumerator ReturnToMenu(float wait)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.LevelComplete();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private string _level;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _requirementText;
    [SerializeField] private GameObject _showLevel;
    [SerializeField] private GameObject _showLock;
    [SerializeField] private Image _image;
    [SerializeField] private Color _completeColor;
    [SerializeField] private Color _lockColor;
    [SerializeField] private Button _button;

    // Start is called before the first frame update
    void Start()
    {
        _levelText.text = _level;
        var levelData = SaveDataManager.Instance.GetLevelData(_level);
        _requirementText.text = "" + levelData.Requirement;
        if (levelData.Unlocked)
        {
            _showLevel.SetActive(true);
            _showLock.SetActive(false);
            _button.enabled = true;
        }
        else
        {
            _showLevel.SetActive(false);
            _showLock.SetActive(true);
            _image.color = _lockColor;
            _button.enabled = false;
        }

        if (levelData.Completed)
        {
            _image.color = _completeColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        GameManager.Instance.LoadLevel(_level);
    }
}

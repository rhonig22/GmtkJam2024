using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private string _level;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    // Start is called before the first frame update
    void Start()
    {
        _levelText.text = _level;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenUXController : MonoBehaviour
{
    private void Start()
    {
        MusicManager.Instance.StartMusic();
    }

    public void PlayButtonClicked()
    {
        GameManager.Instance.LoadOverworld();
    }

    public void SettingsButtonClicked()
    {
        GameManager.Instance.LoadSettings();
    }
}

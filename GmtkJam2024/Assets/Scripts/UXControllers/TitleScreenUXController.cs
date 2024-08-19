using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenUXController : MonoBehaviour
{
    public void PlayButtonClicked()
    {
        GameManager.Instance.LoadOverworld();
    }
}

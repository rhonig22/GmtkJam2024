using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private readonly string _titleScene = "TitleScene";
    private readonly string _settingsScene = "SettingsScene";
    private readonly string _levelScene = "Level";
    private readonly string _overworldScene = "Overworld";
    private string _currentLevel = "";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // called second
    private void OnLevelWasLoaded(int level)
    {
        TransitionManager.Instance.FadeIn(() => { });
    }

    public void LoadSettings()
    {
        LoadScene(_settingsScene);
    }

    public void LoadTitleScreen()
    {
        LoadScene(_titleScene);
    }

    public void LoadOverworld()
    {
        LoadScene(_overworldScene);
    }

    public void LoadLevel(string level)
    {
        _currentLevel = level;
        LoadScene(_levelScene + level);
    }

    public void LevelComplete()
    {
        SaveDataManager.Instance.CompleteLevel(_currentLevel);
        LoadOverworld();
    }

    private void LoadScene(string sceneName)
    {
        UnityAction loadNextBoss = () => { SceneManager.LoadScene(sceneName); };
        TransitionManager.Instance.FadeOut(loadNextBoss);
    }

}
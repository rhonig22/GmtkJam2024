using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private readonly string _titleScene = "TitleScene";
    private readonly string _kitchenScene = "KitchenScene";
    private readonly string _settingsScene = "SettingsScene";
    private readonly string _demoScene = "DemoQTE";
    private readonly string _demoSceneV2 = "DemoV2";
    private readonly string _introStoryScene = "IntroStoryScene";
    private readonly string _recipeBookScene = "RecipeBookScene";
    private readonly string _controlsScene = "ControlsScene";

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

    public void LoadDemo()
    {
        LoadScene(_demoScene);
    }

    public void LoadDemoV2()
    {
        LoadScene(_demoSceneV2);
    }

    public void LoadSettings()
    {
        LoadScene(_settingsScene);
    }

    public void LoadKitchen()
    {
        LoadScene(_kitchenScene);
    }

    public void LoadIntro()
    {
        LoadScene(_introStoryScene);
    }

    public void LoadRecipeBook()
    {
        LoadScene(_recipeBookScene);
    }

    public void LoadTitleScreen()
    {
        LoadScene(_titleScene);
    }

    public void LoadControls()
    {
        LoadScene(_controlsScene);
    }

    private void LoadScene(string sceneName)
    {
        UnityAction loadNextBoss = () => { SceneManager.LoadScene(sceneName); };
        TransitionManager.Instance.FadeOut(loadNextBoss);
    }

}
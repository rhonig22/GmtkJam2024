using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    private readonly string _playerDataKey = "PlayerData";
    private readonly string _levelDataKey = "LevelData";
    private PlayerData _playerData;
    private LevelList _levelList;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        SetUpDataManager();
    }

    private void SetUpDataManager()
    {
        if (PlayerPrefs.HasKey(_playerDataKey))
        {
            _playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(_playerDataKey));
        }
        else
        {
            InitializePlayerData();
        }

        if (PlayerPrefs.HasKey(_levelDataKey))
        {
            _levelList = JsonUtility.FromJson<LevelList>(PlayerPrefs.GetString(_levelDataKey));
        }
        else
        {
            InitializeLevelData();
        }
    }

    public PlayerData GetPlayerData()
    {
        return _playerData;
    }

    public void SetPlayerData(PlayerData playerData)
    {
        PlayerPrefs.SetString(_playerDataKey, JsonUtility.ToJson(playerData));
        PlayerPrefs.Save();
    }

    public void InitializePlayerData()
    {
        PlayerData playerData = new PlayerData()
        {
            SoundFxVolume = 1,
            MusicVolume = 1,
            PlayerName = ""
        };
        SetPlayerData(playerData);
        _playerData = playerData;
    }

    public LevelData GetLevelData(string levelName)
    {
        var vals = levelName.Split('-');
        var world = int.Parse(vals[0]) - 1;
        var level = int.Parse(vals[1]) - 1;
        return _levelList.Worlds[world].Levels[level];
    }

    private void SetLevelList()
    {
        PlayerPrefs.SetString(_levelDataKey, JsonUtility.ToJson(_levelList));
        PlayerPrefs.Save();
    }
    public void CompleteLevel(string level)
    {
        var levelData = GetLevelData(level);
        levelData.Completed = true;
        CheckUnlocks();
        SetLevelList();
    }

    private void CheckUnlocks()
    {
        var totalComplete = 0;
        for (var i = 0; i < _levelList.Worlds.Count; i++)
            for (var j = 0; j < _levelList.Worlds[i].Levels.Count; j++)
            {
                if (_levelList.Worlds[i].Levels[j].Completed)
                    totalComplete++;
            }

        for (var i = 0; i < _levelList.Worlds.Count; i++)
            for (var j = 0; j < _levelList.Worlds[i].Levels.Count; j++)
            {
                var levelData = _levelList.Worlds[i].Levels[j];
                if (levelData.Requirement <= totalComplete)
                    levelData.Unlocked = true;
            }
    }

    public void InitializeLevelData()
    {
        LevelList levelList = new LevelList()
        {
            Worlds = new List<WorldData>()
            {
                new WorldData() {
                    Levels = new List<LevelData> {
                        new LevelData() { Unlocked = true, Completed = false, Requirement = 0 },
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 1},
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 2} ,
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 2} ,
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 3} ,
                    }
                },
                new WorldData() {
                    Levels = new List<LevelData> {
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 4 },
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 5},
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 5} ,
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 6} ,
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 7} ,
                    }
                },
                new WorldData() {
                    Levels = new List<LevelData> {
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 8 },
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 9},
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 9} ,
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 9} ,
                        new LevelData() { Unlocked = false, Completed = false, Requirement = 11} ,
                    }
                },
            }
        };

        _levelList = levelList;
        SetLevelList();
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        SetUpDataManager();
    }
}

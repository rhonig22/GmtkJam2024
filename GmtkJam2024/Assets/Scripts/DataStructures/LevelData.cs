using System;
using System.Collections.Generic;

[Serializable]
public class LevelList
{
    public List<List<LevelData>> Levels;
}

[Serializable]
public class LevelData
{
    public bool Unlocked;
    public bool Completed;
    public int Requirement;
}

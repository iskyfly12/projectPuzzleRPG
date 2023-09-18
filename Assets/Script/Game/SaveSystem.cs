using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    public enum LevelState
    {
        NotCompleted,
        Completed
    }

    public void SaveLevelAsCompleted(int levelID)
    {
        PlayerPrefs.SetInt("level" + levelID, (int)LevelState.Completed);
        PlayerPrefs.Save();
    }

    public bool IsLevelCompleted(int levelID)
    {
        if(!PlayerPrefs.HasKey("level" + levelID))
        {
            PlayerPrefs.SetInt("level" + levelID, (int)LevelState.NotCompleted);
            PlayerPrefs.Save();
        }

        return PlayerPrefs.GetInt("level" + levelID, (int)LevelState.NotCompleted) == (int)LevelState.Completed;
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
    }
}

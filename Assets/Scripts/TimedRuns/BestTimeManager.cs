using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BestTimeManager
{
    public static void SaveBestTime(int levelIndex, float time)
    {
        string levelKey = "Level_" + levelIndex + "_BestTime";
        if (PlayerPrefs.HasKey(levelKey))
        {
            float bestTime = PlayerPrefs.GetFloat(levelKey);
            if (time < bestTime)
            {
                PlayerPrefs.SetFloat(levelKey, time);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(levelKey, time);
        }
    }

    public static float GetBestTime(int levelIndex)
    {
        string levelKey = "Level_" + levelIndex + "_BestTime";
        if (PlayerPrefs.HasKey(levelKey))
        {
            return PlayerPrefs.GetFloat(levelKey);
        }
        return float.MaxValue;
    }
}

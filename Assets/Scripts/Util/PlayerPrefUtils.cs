using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefUtils
{
    public static int GetInt(string key, int defaultValue = 0)
    {
        if(PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        return defaultValue;
    }

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
}
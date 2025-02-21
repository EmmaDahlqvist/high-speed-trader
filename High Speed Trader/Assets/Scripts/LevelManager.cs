using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void SetLastLevel(int level)
    {
        PlayerPrefs.SetInt("LastLevel", level);
        PlayerPrefs.Save();
    }

    public int GetLastLevel()
    {
        return PlayerPrefs.GetInt("LastLevel", 0);
    }
}

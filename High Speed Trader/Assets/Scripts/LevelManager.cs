using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const string CompletedLevelsKey = "CompletedLevels";
    private const string LastLevelKey = "LastLevel";

    public void SetLastLevel(int level)
    {
        PlayerPrefs.SetInt(LastLevelKey, level);
        PlayerPrefs.Save();
    }

    public int GetLastLevel()
    {
        return PlayerPrefs.GetInt(LastLevelKey, 0);
    }

    // Lägg till en ny avklarad level och spara den direkt
    public void AddCompletedLevel(int levelNumber)
    {
        // Hämta nuvarande lista från PlayerPrefs
        List<int> completedLevels = GetCompletedLevels();

        // Lägg till leveln om den inte redan finns
        if (!completedLevels.Contains(levelNumber))
        {
            completedLevels.Add(levelNumber);
            SaveCompletedLevels(completedLevels);
            Debug.Log("Level " + levelNumber + " tillagd och sparad.");
        }
        else
        {
            Debug.Log("Level " + levelNumber + " är redan avklarad.");
        }
    }

    // Hämta alla avklarade levlar från PlayerPrefs
    public List<int> GetCompletedLevels()
    {
        List<int> completedLevels = new List<int>();

        if (PlayerPrefs.HasKey(CompletedLevelsKey))
        {
            string completedLevelsString = PlayerPrefs.GetString(CompletedLevelsKey);
            string[] levelStrings = completedLevelsString.Split(',');

            foreach (string level in levelStrings)
            {
                if (int.TryParse(level, out int levelNumber))
                {
                    completedLevels.Add(levelNumber);
                }
            }
        }

        return completedLevels;
    }

    // Spara listan med avklarade levlar till PlayerPrefs
    private void SaveCompletedLevels(List<int> completedLevels)
    {
        string completedLevelsString = string.Join(",", completedLevels);
        PlayerPrefs.SetString(CompletedLevelsKey, completedLevelsString);
        PlayerPrefs.Save();
    }
}

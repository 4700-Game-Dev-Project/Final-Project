using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMgr : MonoBehaviour
{
    int levelsUnlocked;

    public Button[] lvlButtons;
    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for(int i = 0; i< lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = false;
        }
        Debug.Log("Levels unlocked: " + levelsUnlocked);
        for(int i = 0; i<levelsUnlocked; i++)
        {
            lvlButtons[i].interactable = true;
        }
    }

    public void ResetGame()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        PlayerPrefs.DeleteAll();
        Debug.Log("Levels unlocked reset: " + levelsUnlocked);
    }
}

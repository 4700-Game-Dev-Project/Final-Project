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

        SetUpButtons();
    }

    public void ResetGame()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);
        PlayerPrefs.DeleteAll();
        SetUpButtons();
    }

    private void SetUpButtons()
    {
        for(int i = 0; i< lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = false;
        }
        for(int i = 0; i<levelsUnlocked; i++)
        {
            lvlButtons[i].interactable = true;
        }
    }
}

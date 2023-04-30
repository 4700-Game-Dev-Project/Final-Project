using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class TitleScreenMgr : MonoBehaviour
{

    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene("Level" + sceneNum);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}

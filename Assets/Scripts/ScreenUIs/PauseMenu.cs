using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject escMenu;
    public bool paused;

    void Start()
    {
        escMenu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(paused)
            {
                Resume();
            }
            else
            {
                Debug.Log("reading??");
                Pause();
            }
        }
    }

    public void Pause()
    {
        escMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        escMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}

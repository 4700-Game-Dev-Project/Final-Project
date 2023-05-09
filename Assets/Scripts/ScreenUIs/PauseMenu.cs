using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject escMenu;
    public GameObject dmgImage;
    public static bool paused;
    public AudioSource spaceOnClick;
    EventSystem m_EventSystem;

    void OnEnable()
    {
        m_EventSystem = EventSystem.current;
    }
    void Start()
    {
        escMenu.SetActive(false);
        spaceOnClick.enabled = false;


    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spaceOnClick.enabled = true;
            if (paused)
            {
                spaceOnClick.Play();
                dmgImage.SetActive(true);
                Resume();
            }
            else
            {
                spaceOnClick.Play();
                dmgImage.SetActive(false);
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

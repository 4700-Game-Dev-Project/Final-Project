using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject escMenu;
    public static bool paused;
    public AudioSource spaceOnClick;
    //EventSystem eventSystem = EventSystemManager.currentSystem;
    EventSystem m_EventSystem;

    void OnEnable()
    {
        //Fetch the current EventSystem. Make sure your Scene has one.
        m_EventSystem = EventSystem.current;
    }
    void Start()
    {
        //eventSystem.SetSelectedGameObject( btn, new BaseEventData(eventSystem));
       // var eventSystem = EventSystemManager.currentSystem;
       // eventSystem.SetSelectedGameObject( gameObject, new BaseEventData(eventSystem));
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
                Resume();
            }
            else
            {
                spaceOnClick.Play();
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

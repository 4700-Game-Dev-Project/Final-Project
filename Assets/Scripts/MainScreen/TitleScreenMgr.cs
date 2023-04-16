using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class TitleScreenMgr : MonoBehaviour
{
    public List<Button> levelBtns = new List<Button>();
    public Button exitButton;// = new Button();

    private void Awake()
    {
        SetUpButtons();
    }
    private void LoadScene(int i)
    {
        string sceneNum = (i + 1).ToString();
        Debug.Log("Okay scene is: " + sceneNum);
        SceneManager.LoadScene("Level" + sceneNum);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    private void SetUpButtons()
    {
        for (int i = 0; i < levelBtns.Count; i++)
        {
            int closure = i;
            levelBtns[closure].onClick.AddListener(() => { LoadScene(closure); });
            Debug.Log("I is: " + closure);

            i++;
        }

        exitButton.onClick.AddListener(() => EndGame());
    }
}

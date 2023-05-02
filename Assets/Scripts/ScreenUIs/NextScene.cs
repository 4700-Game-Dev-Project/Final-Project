using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }

    public void SkipIntro()
    {
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }
}

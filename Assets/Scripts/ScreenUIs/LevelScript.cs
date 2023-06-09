using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{

    public Vector3 hitboxDimensions;
    public GameObject endScreen;
    public static bool endScreenActive = false;
    public int currentLevel;
    public LayerMask playerMask;
    public float contactCD = 1.0f; //A cooldown on anything that happens when this object contacts the player
    private bool contactOnCD = false;

    public FinalDoorTrigger finalDoors;
    public NormalKeyDoorTrigger normalKey;
    
    [Header("Player Death Screen")]
    public GameObject deathScreen;

    void Start()
    {
        endScreen.SetActive(false);
        deathScreen.SetActive(false);
        //currentLevel = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("currentLevel: " + currentLevel);
        hitboxDimensions = (transform.localScale * 1.1f) / 2f;

    }

    void FixedUpdate()
    {
        //Detects collision with player based on hitbox
        Collider[] hitbox = Physics.OverlapBox(transform.position, hitboxDimensions, Quaternion.identity, playerMask);
        if (hitbox.Length != 0 )
        {
            StartCoroutine(ActivateEndUI());
        }

    }

    private IEnumerator ActivateEndUI()
    {
        endScreenActive = true;
        contactOnCD = true;
        Pass();
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        yield return new WaitForSeconds(contactCD);
        contactOnCD = false;
    }
    
    private void Pass()
    {

        if(currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
        }

        Debug.Log("level" + PlayerPrefs.GetInt("levelsUnlocked") + " UNLOCKED");
    }

    private void OnTiggerEnter(Collider collision)
    {
         if ((playerMask.value & (1 << collision.transform.gameObject.layer)) > 0) {
                  Debug.Log("Hit with Layermask");
              }
              else {
                  Debug.Log("Not in Layermask");
              }
        Pass();
        endScreen.SetActive(true);
    }

    private void resetKeys()
    {
        finalDoors.setKeyFalse();
        normalKey.setKeyFalse();
    }


    public void LoadNextLevel()
    {
        if(currentLevel < 4)
        {
            resetKeys();
            SceneManager.LoadScene("GameLevel" + (++currentLevel));
            endScreenActive = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            resetKeys();
            SceneManager.LoadScene("ExitStory");
        }
    }

    private IEnumerator ActivateReset()
    {
        resetKeys();
        deathScreen.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("GameLevel" + (currentLevel));
    }

    public void ResetLevel()
    {
        StartCoroutine(ActivateReset());
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

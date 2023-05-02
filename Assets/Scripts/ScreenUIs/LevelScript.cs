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

    void Start()
    {
        endScreen.SetActive(false);
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
        Debug.Log("working??");
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

    public void LoadNextLevel()
    {
        Debug.Log("ok this is before scene load");
        SceneManager.LoadScene("GameLevel" + ++currentLevel);
        Debug.Log("right after scene load");
        endScreenActive = false;
        Debug.Log("and right after deactivating screen");
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

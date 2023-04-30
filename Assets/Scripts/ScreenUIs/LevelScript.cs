using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{

    //Collider exitCollider;
    //public LayerMask playerMask;
    public Vector3 hitboxDimensions;
    //public Canvas canvas;
    public GameObject endScreen;
    int currentLevel;
    public LayerMask playerMask;
    public float contactCD = 1.0f; //A cooldown on anything that happens when this object contacts the player
    private bool contactOnCD = false;

    void Start()
    {
        endScreen.SetActive(false);
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        //canvas = GetComponentInChildren<Canvas>();
        Debug.Log("currentLevel: " + currentLevel);
        //temp = GameObject.Find("EndUI");
        //if(temp != null){
        //If we found the object, get the Canvas component from it.
        //canvas = temp.GetComponent<Canvas>();
        //if(canvas == null)
        //    Debug.Log("Could not locate Canvas component on " + temp.name);
    //}
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
        contactOnCD = true;
        Pass();
        Debug.Log("working??");
        //if(canvas != null)
        //canvas.enabled = true;
        //else
        //    Debug.Log("its null dude");
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        yield return new WaitForSeconds(contactCD);
        contactOnCD = false;
    }
    
    private void Pass()
    {
        //int currentLevel = SceneManager.GetActiveScene().buildIndex;

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
        //TitleScreenMgr.timeScale = 0f;
        endScreen.SetActive(true);
        //canvas.enabled = true;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
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

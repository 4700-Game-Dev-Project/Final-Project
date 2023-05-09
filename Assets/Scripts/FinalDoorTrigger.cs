using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FinalDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private bool isKey = false;
    [SerializeField] private bool is90 = false;

    private static bool hasKey = false;


    public void setKeyFalse()
    {
        hasKey = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isKey)
            {
                hasKey = true;
                gameObject.SetActive(false);
            }

            if (openTrigger && hasKey)
            {
                if (is90)
                {
                    myDoor.Play("door_open_90", 0, 0.0f);
                    gameObject.SetActive(false);
                }
                else
                {
                    myDoor.Play("door_open", 0, 0.0f);
                    gameObject.SetActive(false);
                }
            }
            else if (closeTrigger && hasKey)
            {
                if (is90)
                {
                    myDoor.Play("door_close_90", 0, 0.0f);
                    gameObject.SetActive(false);
                }
                else
                {
                    myDoor.Play("door_close", 0, 0.0f);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

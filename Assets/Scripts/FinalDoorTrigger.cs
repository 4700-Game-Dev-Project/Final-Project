using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FinalDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private bool isKey = false;

    private static bool hasKey = false;


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
                myDoor.Play("door_open", 0, 0.0f);
                gameObject.SetActive(false);
            }
            else if (closeTrigger && hasKey)
            {
                myDoor.Play("door_close", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
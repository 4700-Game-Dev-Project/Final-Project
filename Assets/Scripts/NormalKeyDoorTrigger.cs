using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalKeyDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTriggerA = false;
    [SerializeField] private bool closeTriggerA = false;
    [SerializeField] private bool openTriggerB = false;
    [SerializeField] private bool closeTriggerB = false;
    [SerializeField] private bool isKey = false;
    private static bool openedFromSideA = false;
    private static bool openedFromSideB = false;
    private static bool hasKey = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isKey)
            {
                hasKey = true;
            }

            if (hasKey)
            {
                if (openTriggerA && !openedFromSideB)
                {
                    myDoor.Play("door_open", 0, 0.0f);
                    openedFromSideA = true;
                }
                else if (closeTriggerA && openedFromSideA)
                {
                    myDoor.Play("door_close", 0, 0.0f);
                    openedFromSideA = false;
                }

                if (openTriggerB && !openedFromSideA)
                {
                    myDoor.Play("door_open", 0, 0.0f);
                    openedFromSideB = true;
                }
                else if (closeTriggerB && openedFromSideB)
                {
                    myDoor.Play("door_close", 0, 0.0f);
                    openedFromSideB = false;
                }
            }
        }
    }
}

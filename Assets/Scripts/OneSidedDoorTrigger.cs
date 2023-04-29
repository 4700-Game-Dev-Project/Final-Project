using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSidedDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    
    [SerializeField] private bool openTriggerA = false;
    [SerializeField] private bool closeTriggerA = false;
    [SerializeField] private bool openTriggerB = false;
    [SerializeField] private bool closeTriggerB = false;
    private static bool openedFromOneSide = false;
    private static bool helper = false;
    private static bool openedFromSideA = false;
    private static bool openedFromSideB = false;

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player"))
        {
            if (openTriggerA && !openedFromSideA && !openedFromSideB)
            {
                if (!openedFromOneSide)
                {
                    myDoor.Play("door_open", 0, 0.0f);
                    openedFromOneSide = true;
                    openedFromSideA = true;
                }
                else if (helper)
                {
                    myDoor.Play("door_open", 0, 0.0f);
                    openedFromSideA = true;
                }
            }
            else if (closeTriggerA && openedFromOneSide && openedFromSideA)
            {
                myDoor.Play("door_close", 0, 0.0f);
                openedFromSideA = false;
            }

            if (openTriggerB && !openedFromSideA && !openedFromSideB && openedFromOneSide)
            {
                myDoor.Play("door_open", 0, 0.0f);
                openedFromSideB = true;
            }
            else if (closeTriggerB && openedFromSideB)
            {
                if (openedFromOneSide)
                {
                    myDoor.Play("door_close", 0, 0.0f);
                    openedFromSideB = false;
                    helper = true;
                }
            }
        }
        
    }
}

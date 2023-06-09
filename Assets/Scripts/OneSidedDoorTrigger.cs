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
    [SerializeField] private bool is90 = false;
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
                    if (is90)
                    {
                        myDoor.Play("door_open_90", 0, 0.0f);
                        openedFromOneSide = true;
                        openedFromSideA = true;
                    }
                    else
                    {
                        myDoor.Play("door_open", 0, 0.0f);
                        openedFromOneSide = true;
                        openedFromSideA = true;
                    }
                }
                else if (helper)
                {
                    if (is90)
                    {
                        myDoor.Play("door_open_90", 0, 0.0f);
                        openedFromSideA = true;
                    }
                    else
                    {
                        myDoor.Play("door_open", 0, 0.0f);
                        openedFromSideA = true;
                    }
                }
            }
            else if (closeTriggerA && openedFromOneSide && openedFromSideA)
            {
                if (is90)
                {
                    myDoor.Play("door_close_90", 0, 0.0f);
                    openedFromSideA = false;
                }
                else
                {
                    myDoor.Play("door_close", 0, 0.0f);
                    openedFromSideA = false;
                }
            }

            if (openTriggerB && !openedFromSideA && !openedFromSideB && openedFromOneSide)
            {
                if (is90)
                {
                    myDoor.Play("door_open_90", 0, 0.0f);
                    openedFromSideB = true;
                }
                else
                {
                    myDoor.Play("door_open", 0, 0.0f);
                    openedFromSideB = true;
                }
            }
            else if (closeTriggerB && openedFromSideB)
            {
                if (openedFromOneSide)
                {
                    if (is90)
                    {
                        myDoor.Play("door_close_90", 0, 0.0f);
                        openedFromSideB = false;
                        helper = true;
                    }
                    else 
                    {
                        myDoor.Play("door_close", 0, 0.0f);
                        openedFromSideB = false;
                        helper = true;
                    }
                }
            }
        }
        
    }
}

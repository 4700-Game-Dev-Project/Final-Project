using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Activate : MonoBehaviour
{
    EventSystem m_EventSystem;

    void OnEnable()
    {
        //Fetch the current EventSystem. Make sure your Scene has one.
        m_EventSystem = EventSystem.current;
    }

    void Update()
    {
        //Check if there is a mouse click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //Send a ray from the camera to the mouseposition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Create a raycast from the Camera and output anything it hits
            if (Physics.Raycast(ray, out hit))
                //Check the hit GameObject has a Collider
                if (hit.collider != null)
                {
                    //Click a GameObject to return that GameObject your mouse pointer hit
                    GameObject m_MyGameObject = hit.collider.gameObject;
                    //Set this GameObject you clicked as the currently selected in the EventSystem
                    m_EventSystem.SetSelectedGameObject(m_MyGameObject);
                    //Output the current selected GameObject's name to the console
                    Debug.Log("Current selected GameObject : " + m_EventSystem.currentSelectedGameObject);
                }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    private GameObject player; // Reference of a player
    private AttributesManager attriMan; /// Reference of Attribute Manager attach to this object
    
    /// Implement a timer for speed duration
      
    private void Start()
    {
        attriMan = GetComponent<AttributesManager>();
        player = GameObject.Find("Player");
        if (player == null)
            Debug.Log("Player not found");
    }

    private void OnTriggerEnter(Collider other)
    {
        attriMan.setSpeedToTarget(player);
        Debug.Log("Speed!");
        Destroy(gameObject);
    }
}

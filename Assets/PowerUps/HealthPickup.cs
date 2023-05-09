using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    
    // Start is called before the first frame update
    
    
      private GameObject player; // Reference of a player
    private AttributesManager attriMan; /// Reference of Attribute Manager attach to this object

    private void Start()
    {
        attriMan = GetComponent<AttributesManager>();
        player = GameObject.Find("Player");
        if (player == null)
            Debug.Log("Player not found");
    }

    private void OnTriggerEnter(Collider other)
    {
        attriMan.AddHealthToTarget(player);
        Debug.Log("Heal!");
        Destroy(gameObject);
    }

   
   
        
   

    // Update is called once per frame
   
}

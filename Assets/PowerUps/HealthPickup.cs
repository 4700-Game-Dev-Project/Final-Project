using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int amount1 = 50;
     public int health;
    // Start is called before the first frame update
    
    
    private float CurrentHealth;
    public AttributesManager atm;
    
   

    private void OnTriggerEnter(Collider other) {
            atm = other.GetComponent<AttributesManager>();
            CurrentHealth = atm.GetHealth();
            //atm.addHealth(amount1);
            Destroy(gameObject);
        }
      public void addHealth(int amount){
        if (health + amount > 100)
        {
            health = 100;
            return;
        }
        health += amount;
    }

   
   
        
   

    // Update is called once per frame
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    private float moveSpeedBoost;
    //public Image FillImage;
    private float boostTimer;
    public AttributesManager atm;
    private bool boosting;
    // Start is called before the first frame update
   private void OnTriggerEnter(Collider other) {
     atm = other.GetComponent<AttributesManager>();
          moveSpeedBoost =atm.GetSpeed() * 5;
            //boosting = true;
           // if(boosting){
               // boostTimer += Time.deltaTime;
               // if(boostTimer >=5){
              //  moveSpeedBoost =atm.GetSpeed() * 2;
               // boostTimer = 0;
               // boosting = false;
            //}
            Destroy(gameObject);
    }
             
    //}
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

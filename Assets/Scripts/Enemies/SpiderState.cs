using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderState : MonoBehaviour
{
    private GameObject player;      //refers to the player

    public float contactCD = 1.0f; //A cooldown on anything that happens when this object contacts the player
    private bool contactOnCD = false;

    public Vector3 hitboxDimensions = new Vector3(3f,3f,3f);  //Dimensions of hitbox to detect player collision

    public LayerMask playerMask;

    private AttributesManager attriMan;

    void Start()
    {
        attriMan = GetComponent<AttributesManager>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        //Detects collision with player based on hitbox
        Collider[] hitbox = Physics.OverlapBox(transform.position, hitboxDimensions, Quaternion.identity, playerMask);
        if (hitbox.Length != 0 && contactOnCD == false)
        {
            Debug.Log("Contact");
            StartCoroutine(contactRoutine());
        }
    }

    //Not fully implemented yet; this is so we can have something happen when this object damages the player,
    //like making it stop moving for a second or something. 
    //This could also be used to have "attack speed" for the enemy. 
    private IEnumerator contactRoutine()
    {
        contactOnCD = true;
        //Debug.Log("DMG");
        attriMan.DealDamage(player);

        yield return new WaitForSeconds(contactCD);

        contactOnCD = false;
    }

}

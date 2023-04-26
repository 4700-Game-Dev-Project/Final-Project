using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PartyState : MonoBehaviour
{
    private GameObject player;      //refers to the player
    private Rigidbody m_Rigidbody;

    public float visionRange = 10.0f;

    public float explosionTimer = 3.0f;  // It takes this long to explode
    public float explosionRange = 7.5f;
    private Vector3 explosionDimensions;
    private bool triggered;

    /* These variables handle physical contact with the player, which is not implemented as of now
    public float contactCD = 1.0f; //A cooldown on anything that happens when this object contacts the player
    private bool contactOnCD = false;
    public Vector3 hitboxDimensions;  //Dimensions of hitbox to detect player collision. Does NOT affect pathing or movement
    */

    public LayerMask playerMask;
    public LayerMask obstacleMask;  //Walls and obstacles that this object cannot see through

    public bool seePlayer;

    private AttributesManager attriMan;

    void Start()
    {
        attriMan = GetComponent<AttributesManager>();
        m_Rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        seePlayer = false;
        triggered = false;
        explosionDimensions = new Vector3(explosionRange, explosionRange, explosionRange);
        //hitboxDimensions = (transform.localScale * 1.1f) / 2f;
    }

    void Update()
    {
        StartCoroutine(visionRoutine());

        if (seePlayer && triggered == false)
        {
            StartCoroutine(explosionRoutine());
        }

        /* This is for physical contact with the player in case we end up wanting something to happen with that
        //Detects collision with player based on hitbox
        Collider[] hitbox = Physics.OverlapBox(transform.position, hitboxDimensions, Quaternion.identity, playerMask);
        if (hitbox.Length != 0 && contactOnCD == false)
        {
            StartCoroutine(contactRoutine());
        }*/
    }

    private IEnumerator explosionRoutine()
    {
        Debug.Log("Party Monster triggered.");  //Replace with animation stuff later

        triggered = true;

        yield return new WaitForSeconds(explosionTimer);

        Debug.Log("EXPLOSION"); //Replace with animation stuff later

        Collider[] explosionCheck = Physics.OverlapBox(transform.position, explosionDimensions, Quaternion.identity, playerMask);
        if (explosionCheck.Length != 0)
        {
            if (lineOfSightCheck(explosionCheck[0].transform))
            {
                //Debug.Log("DMG");
                attriMan.DealDamage(player);
            }
        }

        Destroy(gameObject);    //Delete self
    }

    /* This is for physical contact with the player in case we end up wanting something to happen with that
    private IEnumerator contactRoutine()
    {
        contactOnCD = true;
        Debug.Log("DMG");

        yield return new WaitForSeconds(contactCD);

        contactOnCD = false;
    }*/


    //Checks vision every 0.25s. This is done instead of putting it in Update() for the sake of performance
    private IEnumerator visionRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.25f);

        while (true)
        {
            yield return wait;  //waits 0.25 seconds before checking vision
            visionCheck();
        }
    }

    private void visionCheck()
    {
        //Checks whether or not the player is in range to be seen
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, visionRange, playerMask);

        if (rangeCheck.Length != 0)    //If rangeCheck has something in it
        {
            //Check for line of sight to determine whether or not the player is seen
            seePlayer = lineOfSightCheck(rangeCheck[0].transform);
        }

    }

    private bool lineOfSightCheck(Transform target)
    {
        Vector3 directionToPlayer = (target.position - transform.position).normalized;

        //raycastLength becomes the length of the raycast between this object and the player
        //at max range, it should be the same as vision range
        float raycastLength = Vector3.Distance(transform.position, target.position);

        //Shoots a raycast from this object's position to the target's position.
        //If it does NOT hit anything in the layer mask "obstacleMask" then return true
        //This means if there is an obstacle between this object and the target, there is no line of sight
        if (!Physics.Raycast(transform.position, directionToPlayer, raycastLength, obstacleMask))
        {
            return true;
        }
        else    //If the target is behind an obstacle, there is no line of sight; returns false
        {
            return false;
        }

    }

}

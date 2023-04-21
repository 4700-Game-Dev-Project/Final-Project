using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostState : MonoBehaviour
{
    private NavMeshAgent agent;     //refers to NavMeshAgent on self
    private GameObject player;      //refers to the player
    private Rigidbody m_Rigidbody;

    public float visionRange = 15f;
    [Range(0,360)]public float visionAngle = 90f;
    public float rotationSpeed = 3.0f;

    public float chaseTimer = 0.5f;  // This object will still continue chasing the player for chaseTimer seconds after losing vision
    public float contactCD = 1.0f; //A cooldown on anything that happens when this object contacts the player
    private bool contactOnCD = false;

    public Vector3 hitboxDimensions;  //Dimensions of hitbox to detect player collision. Does NOT affect pathing or movement

    public LayerMask playerMask;
    public LayerMask obstacleMask;  //Walls and obstacles that this object cannot see through

    public bool seePlayer;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");

        seePlayer = false;
        hitboxDimensions = (transform.localScale * 1.1f) / 2f;
    }

    void Update()
    {
        //Handles rotation
        Vector3 targetDirection = agent.steeringTarget - transform.position;
        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (seePlayer)
        {
            float countdown = chaseTimer;
            while (countdown >= 0)
            {
                Vector3 playerPos = player.transform.position;
                agent.SetDestination(playerPos);
                countdown -= Time.smoothDeltaTime;
            }
        }

        //Detects collision with player based on hitbox
        Collider[] hitbox = Physics.OverlapBox(transform.position, hitboxDimensions, Quaternion.identity, playerMask);
        if (hitbox.Length != 0 && contactOnCD == false)
        {
            StartCoroutine(contactRoutine());
        }

        StartCoroutine(visionRoutine());
    }

    //Not fully implemented yet; this is so we can have something happen when this object damages the player,
    //like making it stop moving for a second or something. 
    //This could also be used to have "attack speed" for the enemy. 
    private IEnumerator contactRoutine()
    {
        contactOnCD = true;
        Debug.Log("DMG");

        yield return new WaitForSeconds(contactCD);

        contactOnCD = false;
    }


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
            Transform target = rangeCheck[0].transform;    //Set target to the thing in rangeCheck (target is the player)
                                                           //This could also just be set to the player as gotten in Start(),
                                                                //however doing it this way lets this code be reused later
            Vector3 directionToPlayer = (target.position - transform.position).normalized;
            

            //The below gets the angle between the direction to player and the forward facing direction of this object,
                //then sees if that angle is less than what we want. 
            if (Vector3.Angle(transform.forward, directionToPlayer) < visionAngle/2 )
            {
                //raycastLength becomes the length of the raycast between this object and the player
                //at max range, it should be the same as vision range
                float raycastLength = Vector3.Distance(transform.position, target.position);

                //Shoots a raycast from this object's position to the player's position.
                //If it does NOT hit anything in the layer mask "obstacleMask" then seePlayer = true
                    //This means we have checked if the player is in range, in the vision cone, and not behind an obstacle
                if (!Physics.Raycast(transform.position, directionToPlayer, raycastLength, obstacleMask))
                {
                    seePlayer = true;
                }
                else    //If the player is behind an obstacle, seePlayer=false
                {
                    seePlayer = false;
                }
            }
            else    //If the player is not in cone of vision, seePlayer=false
            {
                seePlayer = false;
            }
        }
        else if (seePlayer)     //If the player WAS visible but is no longer in range then seePlayer must be set to false again
        { 
            seePlayer = false;
        }

    }

}

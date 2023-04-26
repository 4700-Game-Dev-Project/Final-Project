using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonState : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;     //refers to NavMeshAgent on self
    private GameObject player;      //refers to the player
    private Rigidbody m_Rigidbody;

    public float visionRange = 15f;
    [Range(0, 360)] public float visionAngle = 90f;
    public float rotationSpeed = 3.0f;

    public float skeletonTimer = 8.0f;   //This is the duration for which Metalon is meant to chase the player, regardless of LoS
    public float chaseTimer = 0.5f;  // This object will still continue chasing the player for chaseTimer seconds after losing vision
    public float contactCD = 1.0f; //A cooldown on anything that happens when this object contacts the player
    private bool contactOnCD = false;

    public Vector3 hitboxDimensions;  //Dimensions of hitbox to detect player collision. Does NOT affect pathing or movement

    public LayerMask playerMask;
    public LayerMask obstacleMask;  //Walls and obstacles that this object cannot see through

    public bool seePlayer;

    //These variables handle patrolling
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    private bool shouldPatrol;
    private bool patrolling;

    private AttributesManager attriMan;

    void Start()
    {
        attriMan = GetComponent<AttributesManager>();
        m_Rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player");

        seePlayer = false;
        shouldPatrol = true;
        patrolling = true;
        hitboxDimensions = (transform.localScale * 1.1f) / 2f;

        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    void FixedUpdate()
    {
        float singleStep = rotationSpeed * Time.deltaTime;
        //Rotate to face the movement direction
        Vector3 targetDirection = agent.steeringTarget - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);


        //Start the player chase if player is visible and Skeleton is on starting position
        if (seePlayer && patrolling)
        {
            patrolling = false;
            StartCoroutine(chaseRoutine());
        }

        //Detects collision with player based on hitbox
        Collider[] hitbox = Physics.OverlapBox(transform.position, hitboxDimensions, Quaternion.identity, playerMask);
        if (hitbox.Length != 0 && contactOnCD == false)
        {
            StartCoroutine(contactRoutine());
        }

        StartCoroutine(visionRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        //If skeleton should patrol
        if (shouldPatrol)
        {
            //Patrol
            if (other.gameObject.CompareTag("Waypoint"))
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }
                agent.SetDestination(waypoints[currentWaypoint].position);
                patrolling = true;
            }
        }
    }

    //Handles chasing the player
    private IEnumerator chaseRoutine()
    {
        shouldPatrol = false; //skeleton should not patrol while chasing

        Debug.Log("Skeleton Chase!");
        float countdown = skeletonTimer;

        while (countdown > 0)
        {
            Vector3 playerPos = player.transform.position;
            agent.SetDestination(playerPos);
            yield return new WaitForSeconds(0.25f);
            countdown -= 0.25f;
        }

        Debug.Log("Skeleton Return.");
        shouldPatrol = true;
        agent.SetDestination(waypoints[currentWaypoint].position); //then go back to patrolling
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


    //Checks vision every 0.25s. This is done instead of putting it in Update() for the sake of performance
    private IEnumerator visionRoutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.25f);  //waits 0.25 seconds before checking vision
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
            if (Vector3.Angle(transform.forward, directionToPlayer) < visionAngle / 2)
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

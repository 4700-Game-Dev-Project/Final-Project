using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlState : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;     //refers to NavMeshAgent on self
    public GameObject player;      //refers to the player
    private Rigidbody m_Rigidbody;

    public float rotationSpeed = 3.0f;

    public Vector3 hitboxDimensions; 

    public LayerMask playerMask;

    public bool shouldChase;
    public bool nearPlayer;

    public bool isMoving;
    private Vector3 lastPosition;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        shouldChase = false;
        hitboxDimensions = (transform.localScale * 4.0f) / 2f;

        isMoving = false;
        lastPosition = transform.position;
    }


    void FixedUpdate()
    {
        /*
        if (transform.position != lastPosition)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = transform.position;*/

        //singleStep is to help handle rotation
        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 targetDirection = agent.steeringTarget - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (shouldChase && !nearPlayer)
        {
            agent.SetDestination(player.transform.position);
            isMoving = true;
        }
        
        if (nearPlayer)
        {
            isMoving = false;
        }

        //Detects collision with player based on hitbox
        Collider[] hitbox = Physics.OverlapBox(transform.position, hitboxDimensions, Quaternion.identity, playerMask);
        if (hitbox.Length != 0)
        {
            shouldChase = true;
            nearPlayer = true;
        }
        else
        {
            nearPlayer = false;
        }
    }

}

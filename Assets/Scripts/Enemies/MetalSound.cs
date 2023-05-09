using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSound : MonoBehaviour
{

    public GameObject parent;
    private MetalonState metalState;
    public AudioSource metalWalk;
    public AudioSource metalRoar;
    private float roarFreq = 5f;
    private float nextRoar;
    void Start()
    {
        metalState = parent.GetComponent<MetalonState>();
        metalWalk.enabled = false;
        metalRoar.enabled = false;
        nextRoar = Time.time + 3f;

    }


    void FixedUpdate()
    {
        if (metalState.isMoving)
        {
            metalWalk.enabled = true;
            if(Time.time > nextRoar)
            {
                metalRoar.enabled = true;
                nextRoar = Time.time + roarFreq;
                metalRoar.Play();
                Debug.Log("Roar");
            }

        }

        else
        {
            metalWalk.enabled = false;
        }
            

        
    }
}

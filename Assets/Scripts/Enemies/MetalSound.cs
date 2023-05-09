using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSound : MonoBehaviour
{

    public GameObject parent;
    private MetalonState metalState;
    public AudioSource metalWalk;
    public AudioSource metalRoar;

    void Start()
    {
        metalState = parent.GetComponent<MetalonState>();
        metalWalk.enabled = false;
        metalRoar.enabled = false;
    }


    void Update()
    {
        if (metalState.isMoving)
            metalWalk.enabled = true;
        else
            metalWalk.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource walkSound, sprintSound, jumpSound;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                walkSound.enabled = false;
                sprintSound.enabled = true;
            }
            else
            {
                walkSound.enabled = true;
                sprintSound.enabled = false;
            }
        }
            
        else
            walkSound.enabled = false;
    }

}

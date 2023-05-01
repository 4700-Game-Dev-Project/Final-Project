using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimator : MonoBehaviour
{
    private Animator animator;
    public GameObject parent;
    private GhostState ghostState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ghostState = parent.GetComponent<GhostState>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Chase", ghostState.isMoving);
    }
}

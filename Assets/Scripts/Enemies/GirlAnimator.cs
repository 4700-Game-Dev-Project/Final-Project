using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlAnimator : MonoBehaviour
{
    private Animator animator;
    public GameObject parent;
    private GirlState girlState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        girlState = parent.GetComponent<GirlState>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalk", girlState.isMoving);
    }
}

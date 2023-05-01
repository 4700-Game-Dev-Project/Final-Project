using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalonAnimator : MonoBehaviour
{
    private Animator animator;
    public GameObject parent;
    private MetalonState metalState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        metalState = parent.GetComponent<MetalonState>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walk Forward", metalState.isMoving);
    }
}

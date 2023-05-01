using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyAnimator : MonoBehaviour
{
    private Animator animator;
    public GameObject parent;
    private PartyState partyState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        partyState = parent.GetComponent<PartyState>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Explode", partyState.triggered);
    }
}

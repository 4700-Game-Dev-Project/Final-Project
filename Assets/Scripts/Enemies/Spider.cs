using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public Transform up, down;
    private GameObject player;      //refers to the player
    public LayerMask playerMask;
    public int speed = 2;
    Vector3 targetPos;
    private AttributesManager attriMan;
    public Vector3 hitboxDimensions;  //Dimensions of hitbox to detect player collision. Does NOT affect pathing or movement
    public float contactCD = 1.0f; //A cooldown on anything that happens when this object contacts the player
    private bool contactOnCD = false;


    void Start()
    {
        targetPos = up.position;
        attriMan = GetComponent<AttributesManager>();
        player = GameObject.Find("Player");
        hitboxDimensions = (transform.localScale * 1.1f) / 2f;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, up.position) < 0.1f)
            targetPos = down.position;

        if(Vector3.Distance(transform.position, down.position) < 0.01f)
            targetPos = up.position;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        //Detects collision with player based on hitbox
        Collider[] hitbox = Physics.OverlapBox(transform.position, hitboxDimensions, Quaternion.identity, playerMask);
        if (hitbox.Length != 0 && contactOnCD == false)
        {
            StartCoroutine(contactRoutine());
        }

    }

    private IEnumerator contactRoutine()
    {
        contactOnCD = true;
        attriMan.DealDamage(player);

        yield return new WaitForSeconds(contactCD);

        contactOnCD = false;
    }

    void OnCollisionEnter(Collision col)
    {
        col.transform.SetParent(transform, true);
    }

    void OnCollisionExit(Collision col)
    {
        col.transform.parent = null;
    }
}

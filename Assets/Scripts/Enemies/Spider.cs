using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public Transform up, down;
    public int speed = 2;
    Vector3 targetPos;

    void Start()
    {
        targetPos = up.position;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, up.position) < 0.1f)
            targetPos = down.position;

        if(Vector3.Distance(transform.position, down.position) < 0.01f)
            targetPos = up.position;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
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

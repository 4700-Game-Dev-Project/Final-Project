using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public int speed = 2;
    private Vector3 downPos;
    private Vector3 upPos;
    private Vector3 targetPos;
    public float maxHeight = 15f;

    void Start()
    {
        downPos = transform.position;
        upPos = new Vector3(downPos.x, downPos.y + maxHeight, downPos.z);
        targetPos = upPos;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, upPos) < 0.1f)
        {
            targetPos = downPos;
        }

        if(Vector3.Distance(transform.position, downPos) < 0.01f)
        {
            targetPos = upPos;
        }

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

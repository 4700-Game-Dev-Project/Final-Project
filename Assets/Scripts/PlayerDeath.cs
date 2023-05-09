using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public AttributesManager attriMan;
    public GameObject exitCollider;
    public LevelScript leveler;

    // Start is called before the first frame update
    void Start()
    {
        attriMan = GetComponent<AttributesManager>();
        exitCollider = GameObject.Find("ExitCollider");
        leveler = exitCollider.GetComponent<LevelScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attriMan.playerDead)
        {
            attriMan.speed = 0;
            leveler.ResetLevel();
        }

    }
}

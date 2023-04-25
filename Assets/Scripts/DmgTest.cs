using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTest : MonoBehaviour
{
    public AttributesManager playerAtm;
    public AttributesManager enemyAtm;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            enemyAtm.DealDamage(playerAtm.gameObject);
        }
    }
}

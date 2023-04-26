using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int attack;

    public void TakeDamage(int amount)
    {
        Debug.Log("Damage Taken: " + amount);
        health -= amount;
        Debug.Log("CurrentHealth: " + health);
    }
    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if(atm != null)
        {
            atm.TakeDamage(attack);
        }
    }

    public int GetHealth()
    {
        return health;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int attack;
    public float speed;

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

    public void AddHealth(int amount)
    {
        if(health + amount > 100)
        {
            health = 100;
            return;
        }
        health += amount;
        
    }

    public void setHealth(int amount)
    {
        health = amount;
    }

    public void setSpeed(float s)
    {
        speed = s;
    }


    public float GetSpeed()
    {
        return speed;
    }
    public int GetHealth()
    {
        return health;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int attack;
    public float speed;
    public int healAmount;


    private void TakeDamage(int amount)
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

    public void AddHealthToTarget(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if(atm != null)
        {
            atm.addHealth(healAmount);
        }
        
    }

    private void addHealth(int amount)
    {
        if (health + amount > 100)
        {
            health = 100;
            return;
        }
        health += amount;
    }

    public void setHealthToTarget(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            atm.setHealth(healAmount);
        }
    }
    
    private void setHealth(int amount)
    {
        health = amount;
    }

    public void setSpeedToTarget(GameObject target, int s)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            atm.setSpeed(healAmount);
        }
    }

    private void setSpeed(int s)
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

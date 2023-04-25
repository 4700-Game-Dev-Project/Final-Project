using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image FillImage;
    private float CurrentHealth;
    public AttributesManager playerAtm;
    
    private void Start()
    {
        //playerAtm = GetComponent<AttributesManager>();
        CurrentHealth = playerAtm.GetHealth();
    }


    private void Update()
    {
        UpdateHP();
    }

    public void UpdateHP()
    {
        CurrentHealth = playerAtm.GetHealth();
        FillImage.fillAmount = CurrentHealth / 100;
    }
}

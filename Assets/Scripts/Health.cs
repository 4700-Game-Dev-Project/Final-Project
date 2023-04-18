using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image FillImage;
    public float CurrentHealth;

    public void UpdateHP()
    {
        FillImage.fillAmount = CurrentHealth / 100;
        Debug.Log("clicked");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            CurrentHealth+=0.05f;
            UpdateHP();
        }
        else if (Input.GetKey(KeyCode.O))
        {
            CurrentHealth -= 0.05f;
            UpdateHP();
        }
    }
}

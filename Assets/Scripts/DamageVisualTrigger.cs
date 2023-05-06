using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageVisualTrigger : MonoBehaviour
{

    public GameObject _onHitScreenVisual;
    public AttributesManager atmPlayer;

    private int playersHealth;

    private void Start()
    {
        playersHealth = atmPlayer.GetHealth();

    }

    private void checkHDecreased()
    { // triggers visual on hit when player's health decreases
        if(playersHealth > atmPlayer.GetHealth())
        {
            Debug.Log("player damaged");
            gotHurt();
            playersHealth = atmPlayer.GetHealth();
        }
    }

    private void checkHIncreased()
    { //Updates when players health is regen
        if (playersHealth < atmPlayer.GetHealth())
        {
            Debug.Log("player healed");
            playersHealth = atmPlayer.GetHealth();
        }
    }


    private void FixedUpdate()
    {

        checkHDecreased();
        checkHIncreased();

        if(_onHitScreenVisual != null)
        {
            if(_onHitScreenVisual.GetComponent<Image>().color.a > 0)
            {

                var color = _onHitScreenVisual.GetComponent<Image>().color;
                color.a -= 0.01f;
               _onHitScreenVisual.GetComponent<Image>().color = color;
            }
        }
    }

    private void gotHurt()
    {
         var imageColor = _onHitScreenVisual.GetComponent<Image>().color;
         imageColor.a = 0.5f;

        _onHitScreenVisual.GetComponent<Image>().color = imageColor;


    }


}

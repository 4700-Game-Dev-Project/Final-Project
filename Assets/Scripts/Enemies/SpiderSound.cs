using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource spiderSound;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SpiderTrigger");
        print(other.tag);
        if (other.tag == "Player" && !spiderSound.isPlaying)
        {
            
            Debug.Log("Human you entered my zone! said spider");
            Debug.Log("HISS!");
            spiderSound.Play();
        }
    }



}

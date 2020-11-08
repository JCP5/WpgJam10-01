using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public bool lightOn;
    public bool playerNearby;

    void Update()
    {
        if(playerNearby == true && Input.GetKeyDown(KeyCode.E))
        {
            SwitchLight();
        }
    }

    public void SwitchLight()
    {
        if(lightOn == false)
        {
            lightOn = true;
            LevelManager.instance.lightsOn++;
        }
        else if(lightOn == true)
        {
            lightOn = false;
            LevelManager.instance.lightsOn--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}

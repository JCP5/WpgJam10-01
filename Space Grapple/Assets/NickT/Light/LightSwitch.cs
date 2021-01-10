using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public LightScript[] ControlledLights;
    public bool flipped;

    public bool playerNearby;

    // Start is called before the first frame update
    void Start()
    {
        if (flipped)
        {
            for (int i = 0; i < ControlledLights.Length; i++)
            {
                ControlledLights[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < ControlledLights.Length; i++)
            {
                ControlledLights[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerNearby == true)
        {
            FlipSwitch();
        }
    }

    //Flip the switch 
    void FlipSwitch()
    {
        Transform handle = transform.Find("Handle");

        if (flipped)
        {
            flipped = false;

            handle.localRotation = Quaternion.Euler(90, 0, 0);
            FlipLights(flipped);
        }
        else
        {
            flipped = true;

            handle.localRotation = Quaternion.Euler(270, 0, 0);
            FlipLights(flipped);
        }
    }

    void FlipLights(bool state)
    {
        for (int i = 0; i < ControlledLights.Length; i++)
        {
            ControlledLights[i].enabled = state;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        playerNearby = false;
    }
}

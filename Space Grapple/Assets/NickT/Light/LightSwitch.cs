using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public LightScript[] ControlledLights;
    public bool flipped;

    public Material handleMat;
    public Color color;

    public bool playerNearby;

    // Start is called before the first frame update
    void Start()
    {
        handleMat = transform.Find("Handle").GetComponent<MeshRenderer>().material;
        ControlledLights = GetComponentsInChildren<LightScript>();
        IntializeSwitch(flipped);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerNearby == true)
        {
            FlipSwitch();
        }
        handleMat.color = color;
    }

    //Flip the switch 
    void FlipSwitch()
    {
        Transform handle = transform.Find("Handle");

        if (flipped)
        {
            flipped = false;

            handle.localRotation = Quaternion.Euler(90, 90, 0);
        }
        else
        {
            flipped = true;

            handle.localRotation = Quaternion.Euler(270, 90, 0);
        }

        UpdateLights(flipped);
    }

    void IntializeSwitch(bool state)
    {
        foreach (LightScript ls in ControlledLights)
        {
            ls.UpdateLightState(state);
        }
    }

    void UpdateLights(bool state)
    {
        foreach(LightScript ls in ControlledLights)
        {
            ls.SwitchLight(state);
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

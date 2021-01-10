using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public bool lightOn;
    public bool playerNearby;

    public Light lightComponent;

    public void SwitchLight()
    {
        if(lightOn == false)
        {
            lightOn = true;
            LevelManager.instance.UpdateLights(lightOn);
        }
        else if(lightOn == true)
        {
            lightOn = false;
            LevelManager.instance.UpdateLights(lightOn);
        }
    }

    public void LightState(bool state)
    {
        if (state)
        {
            lightComponent.enabled = state;
        }
    }
}

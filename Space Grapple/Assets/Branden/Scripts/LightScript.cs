using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    float defaultIntensity;

    public LightSwitch parentSwitch;
    public Light lightObject;

    private void Start()
    {
        parentSwitch = GetComponentInParent<LightSwitch>();
        lightObject = this.GetComponent<Light>();
        defaultIntensity = lightObject.intensity;
        UpdateLightState(parentSwitch.flipped);
    }

    private void Update()
    {
        lightObject.color = parentSwitch.handleMat.color;
        UpdateLightBulbMat();
    }

    public void SwitchLight(bool lightSwitchState)
    {
        UpdateLightState(lightSwitchState);
        LevelManager.instance.UpdateLights(lightSwitchState);
    }

    public void UpdateLightState(bool state)
    {
        Debug.Log("hello " + parentSwitch.name);
        if (state)
        {
            lightObject.intensity = defaultIntensity;
        }
        else
        {
            lightObject.intensity = 0;
        }
    }

    public bool GetParentSwitchState()
    {
        return parentSwitch.flipped;
    }

    void UpdateLightBulbMat()
    {
        GameObject lightBulbObject = transform.Find("marine_light2").transform.Find("LightBulb").gameObject;
        MeshRenderer lightBulbMat = lightBulbObject.GetComponent<MeshRenderer>();
        Material mat = lightBulbMat.material;

        mat.color = lightObject.color;

        if (lightObject.intensity > 0)
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", lightObject.color);
        }
        else
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.black);
        }
    }
}

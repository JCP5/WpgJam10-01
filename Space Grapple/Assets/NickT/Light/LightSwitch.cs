using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public LightScript[] ControlledLights;
    public bool Flipped;

    // Start is called before the first frame update
    void Start()
    {
        if (Flipped)
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

    }

    //Flip the switch 
    void FlipSwitch()
    {
        if (Flipped)
        {
            Flipped = false;

            this.transform.RotateAround(transform.position, transform.right, 180f);

            for (int i = 0; i < ControlledLights.Length; i++)
            {
                ControlledLights[i].enabled = false;
            }
        }
        else
        {
            Flipped = true;

            this.transform.RotateAround(transform.position, transform.right, 180f);

            for (int i = 0; i < ControlledLights.Length; i++)
            {
                ControlledLights[i].enabled = true;
            }
        }
    }
}

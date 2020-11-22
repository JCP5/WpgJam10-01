using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{

    public Light[] ControlledLights;
    public bool Flipped;

    public AudioSource FlipNoise;
    public AudioSource LightOnNoise;

    int interval = 2;
    float current_time = 0;

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
        current_time += Time.deltaTime;
        if (current_time >= interval)
        {
            //do something here every interval seconds
            FlipSwitch();
            current_time = 0.0f;
        }
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

            FlipNoise.Play();
        }
        else
        {
            Flipped = true;

            this.transform.RotateAround(transform.position, transform.right, 180f);

            for (int i = 0; i < ControlledLights.Length; i++)
            {
                ControlledLights[i].enabled = true;
            }

            FlipNoise.Play();
            LightOnNoise.Play();
        }
    }


}

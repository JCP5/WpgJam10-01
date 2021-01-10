using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LightScript[] lightsInScene;
    public int numOfLights;
    public int lightsOn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        numOfLights = lightsInScene.Length;
    }

    void Start()
    {
        foreach (LightScript ls in lightsInScene)
        {
            if (ls.lightOn == true)
            {
                lightsOn++;
            }
        }
    }

    void Update()
    {

    }

    void LevelClear()
    {
        if (lightsOn == numOfLights)
        {
            Debug.Log("Level Clear");
        }
    }

    public void UpdateLights(bool state)
    {
        if (state == true)
        {
            lightsOn++;
            LevelClear();
        }
        else
        {
            lightsOn--;
            LevelClear();
        }
    }
}

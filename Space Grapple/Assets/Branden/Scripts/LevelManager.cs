using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LightScript[] lightsInScene;

    public int nextLevel = 0;

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

        lightsInScene = FindObjectsOfType<LightScript>();
    }

    void Start()
    {
        numOfLights = lightsInScene.Length;
        InitializeOnLights();
    }

    void InitializeOnLights()
    {
        foreach (LightScript ls in lightsInScene)
        {
            if (ls.GetParentSwitchState() == true)
            {
                lightsOn++;
            }
        }
    }

    public void LevelClear()
    {
        if (lightsOn >= numOfLights)
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

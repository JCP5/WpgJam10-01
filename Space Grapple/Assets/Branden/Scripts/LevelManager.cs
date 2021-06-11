using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LightScript[] lightsInScene;

    public scenenavigation sceneNavigator;

    public int nextLevel = 0;

    public int numOfLights;
    public int lightsOn;
    public bool levelClear = false;

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

        scenenavigation temp = FindObjectOfType<scenenavigation>();
        if (temp == null)
        {
            Instantiate(sceneNavigator, Vector3.zero, Quaternion.identity);
        }

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
            levelClear = true;

            SpaceShipDoor door = FindObjectOfType<SpaceShipDoor>();
            door.OpenDoor();
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

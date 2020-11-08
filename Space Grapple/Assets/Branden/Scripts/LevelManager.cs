using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
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
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(lightsOn == numOfLights)
        {
            LevelClear();
        }
    }

    void LevelClear()
    {
        Debug.Log("Level Clear");
    }
}

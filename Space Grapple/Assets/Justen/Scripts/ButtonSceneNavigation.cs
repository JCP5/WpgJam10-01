using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ButtonSceneNavigation : MonoBehaviour
{
    Text buttonText;

    private void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        buttonText.text = this.gameObject.name;
    }

    public void LoadLevelByButtonName()
    {
        SceneManager.LoadScene(Int32.Parse(this.gameObject.name));
    }

    public void LoadLevelByInt(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void LoadLevelByname(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}

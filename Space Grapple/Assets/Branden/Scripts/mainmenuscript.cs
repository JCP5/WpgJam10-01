using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class mainmenuscript : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject howToPlayScreen;
    public GameObject creditsScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenHowToPlay()
    {
        mainMenuScreen.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void OpenCredits()
    {
        mainMenuScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void BackButton()
    {
        creditsScreen.SetActive(false);
        howToPlayScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

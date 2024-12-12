using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("FinalProject");
        // restarts game mechanics
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}

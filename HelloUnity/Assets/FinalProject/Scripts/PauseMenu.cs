using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;

    public void Pause()
    {
        // activate pause screen
        pauseScreen.SetActive(true);
        // pause game mechanics
        Time.timeScale = 0;
    }

    public void Continue ()
    {
        // de activate pause screen
        pauseScreen.SetActive(false);
        // resume game mechanics
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        // restarts game mechanics
        Time.timeScale = 1;
    }
}

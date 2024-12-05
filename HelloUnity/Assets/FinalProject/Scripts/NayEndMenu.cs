using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NayEndMenu : MonoBehaviour
{
    // reference to heart lives
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    // reference to NayEndScreen
    public GameObject nayScreen;

    // Update is called once per frame
    void Update()
    {
        // check if hearts are inactive
        if (!heart1.activeSelf && !heart2.activeSelf && !heart3.activeSelf)
        {
            // activate NayEndScreen
            nayScreen.SetActive(true);

            // disable script to prevent further checks
            this.enabled = false;

            // pause game mechanics
            Time.timeScale = 0;
        }
    }

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
}

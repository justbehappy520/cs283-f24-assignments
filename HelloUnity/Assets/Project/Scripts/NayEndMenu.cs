using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NayEndMenu : MonoBehaviour
{
    // reference to NayEndScreen
    public GameObject nayScreen;
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

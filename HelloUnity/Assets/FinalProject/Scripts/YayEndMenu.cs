using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YayEndMenu : MonoBehaviour
{
    public GameObject gate; // gate collider
    public GameObject key; // key collider
    public GameObject yayScreen; // YayEndScreen

    private void OnTriggerEnter(Collider other)
    {
        // check if the player collided with the gate
        if (other.CompareTag("Player") && !key.activeSelf)
        {
            // activate yayScreen
            yayScreen.SetActive(true);

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

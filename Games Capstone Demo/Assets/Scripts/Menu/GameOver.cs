using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    public Button retryBtn;
    public Button mainMenuBtn;

    private Pause pauseScript;
    private bool playerDead = false;

    // Set up variables, find scripts, and enable buttons
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            pauseScript = gameController.GetComponent<Pause>();
        }
        else
        {
            Debug.Log("Error. Couldn't find Game Controller");
        }

        Button btnRetry = retryBtn.GetComponent<Button>();
        btnRetry.onClick.AddListener(RetryOnClick);

        Button btnMainMenu = mainMenuBtn.GetComponent<Button>();
        btnMainMenu.onClick.AddListener(MainMenuOnClick);

        gameOverMenuUI.SetActive(false);
    }

    // Reload on button press
    void RetryOnClick()
    {
        if (playerDead == true)
        {
            Reload();
        }
    }

    // Load Main Menu scene when menu button is clicked
    void MainMenuOnClick()
    {
        SceneManager.LoadScene(0);
    }

    // Reloads the current scene
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // Turns on Game over UI, pauses game and music
    public void playerDied()
    {
        pauseScript.disablePause();
        playerDead = true;
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameObject.GetComponent<AudioSource>().Pause();
    }
}

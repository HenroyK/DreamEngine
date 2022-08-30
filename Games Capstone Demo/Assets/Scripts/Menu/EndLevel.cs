using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    public GameObject endLevelMenuUI;
    public Button reloadlevelBtn;
    public Button mainMenuBtn;

    private GameContollerScript gameControllerScript;
    private Pause pauseScript;

    // Set up variables, find scripts, and enable buttons
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            pauseScript = gameController.GetComponent<Pause>();
            gameControllerScript = gameController.GetComponent<GameContollerScript>();
        }
        else
        {
            Debug.Log("Error. Couldn't find Game Controller");
        }

        Button btnRetry = reloadlevelBtn.GetComponent<Button>();
        btnRetry.onClick.AddListener(ReloadOnClick);

        Button btnMainMenu = mainMenuBtn.GetComponent<Button>();
        btnMainMenu.onClick.AddListener(MainMenuOnClick);

        endLevelMenuUI.SetActive(false);
    }

    // Reload on button press
    void ReloadOnClick()
    {
        Reload();
    }

    // Load Main Menu scene when menu button press
    void MainMenuOnClick()
    {
        SceneManager.LoadScene(0);
    }

    // Reloads the current scene
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Turns on end game UI, pauses game and music
    public void EndLevelReached()
    {
        pauseScript.disablePause(); // disable pause functionality
        gameControllerScript.PlayerControls(false); // disable player controls
        endLevelMenuUI.SetActive(true); // enable game over UI
        Time.timeScale = 0; // pause game
        // pause game music (attached to game controller object)
        gameObject.GetComponent<AudioSource>().Pause();
    }
}

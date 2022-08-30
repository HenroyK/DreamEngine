using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeBtn;
    public Button mainMenuBtn;

    private GameContollerScript gameControllerScript;
    private bool gamePaused;
    private bool canPause;

    private int numberOfOptions = 2;
    private int selectedOption;

    // Game starts unpaused (running)
    // Set up the button variables for use
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            gameControllerScript = gameController.GetComponent<GameContollerScript>();
        }
        else
        {
            Debug.Log("Error. Couldn't find Game Controller");
        }

        Button btnResume = resumeBtn.GetComponent<Button>();
        btnResume.onClick.AddListener(ResumeOnClick);

        Button btnMainMenu = mainMenuBtn.GetComponent<Button>();
        btnMainMenu.onClick.AddListener(MainMenuOnClick);

        ResumeGame();
        enablePause();

        selectedOption = 1;
        // position selected highlight Icon/shade
    }

    // Game is paused and unpaused with a toggle control (key)
    void Update()
    {
        if (Input.GetButtonDown("Pause") && canPause)
        {
            TogglePause();
        }

        if (gamePaused)
        {
            // select options code
            if (Input.GetButtonDown("SwapForward") /*|| Input.GetAxis("SwapForward") > 0*/)
            {
                selectedOption += 1;
                if (selectedOption > numberOfOptions)
                {
                    selectedOption = 1;
                }

                // reset selected highlight
                SwapSelected(selectedOption);
            }

            if (Input.GetButtonDown("SwapBackward") /*|| Input.GetAxis("SwapBackward") > 0*/)
            {
                selectedOption -= 1;
                if (selectedOption < 1)
                {
                    selectedOption = numberOfOptions;
                }

                // reset selected highlight
                SwapSelected(selectedOption);
            }

            if (Input.GetButton("Jump"))
            {

                switch (selectedOption)
                {
                    case 1:
                        ResumeOnClick();
                        break;
                    case 2:
                        MainMenuOnClick();
                        break;
                }
            }
        }
    }

    void SwapSelected(int option)
    {
        // reset selected highlight

        Debug.Log("Picked: " + selectedOption);
        switch (selectedOption)
        {
            case 1:
                /*Do option two*/
                break;
            case 2:
                /*Do option two*/
                break;
        }
    }

    // Manages the pause state of the game,
    // if game is pause -> unpause, else pause
    void TogglePause()
    {
        if (gamePaused == false)
        {
            PauseGame();
        }
        else if (gamePaused == true)
        {
            ResumeGame();
        }
    }

    // Unpauses when resume button is clicked
    void ResumeOnClick()
    {
        TogglePause();
    }

    // Load Main Menu scene when menu button is clicked
    void MainMenuOnClick()
    {
        SceneManager.LoadScene(0);
    }

    // pauses game and music, loads respective UI
    void PauseGame()
    {
        gamePaused = true;
        pauseMenuUI.SetActive(true); // enable pause UI
        gameControllerScript.PlayerControls(false); // disable player controls
        Time.timeScale = 0; // pause game
        // pause game music (attached to game controller object)
        gameObject.GetComponent<AudioSource>().Pause(); 
    }

    //unpauses game and music, hides respective UI
    void ResumeGame()
    {
        gamePaused = false;
        pauseMenuUI.SetActive(false); // disable pause UI
        gameControllerScript.PlayerControls(true); // enable player controls
        Time.timeScale = 1; // unpause game
        // unpause game music (attached to game controller object)
        gameObject.GetComponent<AudioSource>().UnPause(); 
    }

    public void disablePause()
    {
        canPause = false;
    }

    public void enablePause()
    {
        canPause = true;
    }
}

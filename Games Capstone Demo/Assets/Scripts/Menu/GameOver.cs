using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    public Button retryBtn;
    public Button mainMenuBtn;
    public GameObject btnHighlight;

    private GameContollerScript gameControllerScript;
    private Pause pauseScript;
    private bool playerDead = false;

    private int numberOfOptions = 2;
    private int selectedOption;

    private float inputTimer = 0;
    [SerializeField]
    private float waitTime = 0.3f;

    // Set up variables, find scripts, and enable buttons
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            pauseScript = gameController.GetComponent<Pause>();
            gameControllerScript = 
                gameController.GetComponent<GameContollerScript>();
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

        selectedOption = 1;
        btnHighlight.transform.position =
                    retryBtn.transform.position;
        btnHighlight.SetActive(false);
    }

    void Update()
    {
        inputTimer += Time.unscaledDeltaTime;

        if (playerDead && inputTimer >= waitTime)
        {
            // select options code
            if (Input.GetAxisRaw("Swap") > 0)
            {
                selectedOption += 1;
                if (selectedOption > numberOfOptions)
                {
                    selectedOption = 1;
                }

                // reset selected highlight
                SwapSelected(selectedOption);
            }

            if (Input.GetAxisRaw("Swap") < 0)
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
                        RetryOnClick();
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
                btnHighlight.transform.position =
                        retryBtn.transform.position;
                break;
            case 2:
                btnHighlight.transform.position =
                        mainMenuBtn.transform.position;
                break;
        }
        inputTimer = 0;
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
        pauseScript.disablePause(); // disable pause functionality
        playerDead = true;
        gameOverMenuUI.SetActive(true); // enable game over UI
        gameControllerScript.PlayerControls(false); // disable player controls
        // pause game music (attached to game controller object)
        gameObject.GetComponent<AudioSource>().Pause();
        btnHighlight.SetActive(true);
        Time.timeScale = 0; // pause game
    }

    // Mouse over Retry button
    public void MORetryBtn()
    {
        selectedOption = 1;
        SwapSelected(selectedOption);
    }

    // Mouse over Menu button
    public void MOMenuBtn()
    {
        selectedOption = 2;
        SwapSelected(selectedOption);
    }
}

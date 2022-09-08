using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeBtn;
    public Button mainMenuBtn;
    public GameObject btnHighlight;

    private GameContollerScript gameControllerScript;
    private bool gamePaused;
    private bool canPause;

    // button select varaibles
    private int numberOfOptions = 2;
    private int selectedOption;

    private float inputTimer = 0;
    [SerializeField]
    private float waitTime = 0.3f;

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
        btnHighlight.transform.position =
                    resumeBtn.transform.position;
        btnHighlight.SetActive(false);
        // position selected highlight Icon/shade
    }

    // Game is paused and unpaused with a toggle control (key)
    void Update()
    {
        inputTimer += Time.unscaledDeltaTime;

        if (gamePaused && inputTimer >= waitTime)
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
                        ResumeOnClick();
                        break;
                    case 2:
                        MainMenuOnClick();
                        break;
                }
            }
        }

        if (Input.GetButtonDown("Pause") && canPause)
        {
            TogglePause();
        }
    }

    void SwapSelected(int option)
    {
        // reset selected highlight

        Debug.Log("Picked: " + selectedOption);

        if (btnHighlight != null)
        {
            switch (selectedOption)
            {
                case 1:
                    btnHighlight.transform.position =
                        resumeBtn.transform.position;
                    break;
                case 2:
                    btnHighlight.transform.position =
                        mainMenuBtn.transform.position;
                    break;
            }
        }
        inputTimer = 0;
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

    // Mouse over select highlight
    //bool IsMouseOverUI()
    //{
    //    return EventSystem.current.IsPointerOverGameObject();
    //}

    //bool IsMouseOverUIwithIgnores()
    //{
    //    PointerEventData pED
    //}

    // pauses game and music, loads respective UI
    void PauseGame()
    {
        gamePaused = true;
        pauseMenuUI.SetActive(true); // enable pause UI
        gameControllerScript.PlayerControls(false); // disable player controls
        // pause game music (attached to game controller object)
        gameObject.GetComponent<AudioSource>().Pause();
        btnHighlight.SetActive(true);

        Time.timeScale = 0; // pause game
    }

    //unpauses game and music, hides respective UI
    void ResumeGame()
    {
        gamePaused = false;
        pauseMenuUI.SetActive(false); // disable pause UI
        gameControllerScript.PlayerControls(true); // enable player controls
        // unpause game music (attached to game controller object)
        gameObject.GetComponent<AudioSource>().UnPause();
        btnHighlight.SetActive(false);

        Time.timeScale = 1; // unpause game
    }

    //Mouse over Resume button
    public void MOResumeBtn()
    {
        selectedOption = 1;
        SwapSelected(selectedOption);
    }

    //Mouse over Main Menu button
    public void MOMenuBtn()
    {
        selectedOption = 2;
        SwapSelected(selectedOption);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool gamePaused;

    // Game starts unpaused (running)
    void Start()
    {
        ResumeGame();
    }

    // Game is paused and unpaused with a toggle control (key)
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
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
    }

    void PauseGame()
    {
        gamePaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        gamePaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
}

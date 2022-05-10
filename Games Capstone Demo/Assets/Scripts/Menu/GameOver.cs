using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    public Button retryBtn;

    private Pause pauseScript;
    private bool playerDead = false;

    // Set up variables / find scripts
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

        Button btn = retryBtn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        gameOverMenuUI.SetActive(false);
    }

    // Reload on button press
    void TaskOnClick()
    {
        if (playerDead == true)
        {
            Reload();
        }
    }

    // Reloads the current scene
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // Turns on Game over UI
    public void playerDied()
    {
        pauseScript.disablePause();
        playerDead = true;
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
}

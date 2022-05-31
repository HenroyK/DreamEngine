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

    private Pause pauseScript;

    // Start is called before the first frame update
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

        Button btnRetry = reloadlevelBtn.GetComponent<Button>();
        btnRetry.onClick.AddListener(ReloadOnClick);

        Button btnMainMenu = mainMenuBtn.GetComponent<Button>();
        btnMainMenu.onClick.AddListener(MainMenuOnClick);

        endLevelMenuUI.SetActive(false);
    }

    void ReloadOnClick()
    {
        Reload();
    }

    void MainMenuOnClick()
    {
        SceneManager.LoadScene(0);
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndLevelReached()
    {
        pauseScript.disablePause();
        endLevelMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameObject.GetComponent<AudioSource>().Pause();
    }
}

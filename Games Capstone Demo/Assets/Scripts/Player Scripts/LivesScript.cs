using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesScript : MonoBehaviour
{
    public float lifeCount = 3;
    public float lifeCountCap = 3;

    public GameObject livesUI;
    public string defaultText = "Lives: ";
    public GameObject livesText;

    private GameContollerScript gameControllerScript;
    private GameOver gameOverScript;

    // Set up variables / find scripts
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            gameControllerScript = gameController.GetComponent<GameContollerScript>();
            gameOverScript = gameController.GetComponent<GameOver>();
        }
        else
        {
            Debug.Log("Error. Couldn't find Game Controller");
        }

        livesUI.SetActive(true);

        RefreshUI();

        Debug.Log(livesText.GetComponent<TextMeshProUGUI>().text);
    }

    void RefreshUI()
    {
        livesText.GetComponent<TextMeshProUGUI>().text = defaultText + lifeCount;
    }

    // Add health
    public void LifeCountGain(float heal)
    {
        for (int i = 0; i < (int)heal; i++)
        {
            if (lifeCount < lifeCountCap)
            {
                lifeCount += 1;
            }
            else
            {
                break;
            }
        }

        RefreshUI();
    }

    // Remove health
    public void LifeCountLoss(float damage)
    {
        for (int i = 0; i < (int)damage; i++)
        {
            lifeCount -= 1;

            if (lifeCount <= 0)
            {
                gameOverScript.playerDied();

                return;
            }
        }

        gameControllerScript.LoadCheckpoint();

        RefreshUI();
    }
}

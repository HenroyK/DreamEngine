using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifesScript : MonoBehaviour
{
    public float lifeCount = 3;     // current lives
    public float lifeCountCap = 3;  // maximum possible lives

    // GUI global variables
    public GameObject livesUI;
    public string defaultText = "Lives: ";
    public GameObject livesText;

    public GameObject heartA;
    public GameObject heartB;
    public GameObject heartC;

    private GameContollerScript gameControllerScript;
    private GameOver gameOverScript;

    // Set up variables and find scripts
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

        if (gameControllerScript.enableLifes)
        {
            livesUI.SetActive(true);

            RefreshUI();

            Debug.Log(livesText.GetComponent<TextMeshProUGUI>().text);
        }
    }

    void RefreshUI()
    {
        //livesText.GetComponent<TextMeshProUGUI>().text = defaultText + lifeCount;
        switch(lifeCount)
        {
            case 3:
                heartA.SetActive(true);
                heartB.SetActive(true);
                heartC.SetActive(true);
                break;
            case 2:
                heartA.SetActive(true);
                heartB.SetActive(true);
                heartC.SetActive(false);
                break;
            case 1:
                heartA.SetActive(true);
                heartB.SetActive(false);
                heartC.SetActive(false);
                break;
            case 0:
                heartA.SetActive(false);
                heartB.SetActive(false);
                heartC.SetActive(false);
                break;
            default:
                break;
        }
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

    // Remove health and return them to checkpoint,
    // trigger end game function in game over script if life count is zero
    public void LifeCountLoss(float damage)
    {
        for (int i = 0; i < (int)damage; i++)
        {
            lifeCount -= 1;

            if (lifeCount <= 0)
            {
                RefreshUI();

                gameOverScript.playerDied();

                return;
            }
        }

        gameControllerScript.LoadCheckpoint();

        RefreshUI();
    }
}

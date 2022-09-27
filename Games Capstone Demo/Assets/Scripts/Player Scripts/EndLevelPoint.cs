using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelPoint : MonoBehaviour
{
    private EndLevel endLevelScript;
    private BlackFade fadeScript;

    // Get a reference of EndLevel script attached to the scene game controller script
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            endLevelScript = gameController.GetComponent<EndLevel>();
            fadeScript = gameController.GetComponent<BlackFade>();
        }
        else
        {
            Debug.Log("Error. Couldn't find Game Controller");
        }
    }

    // Triggers EndLevel script function if the player collides with the attached object
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //endLevelScript.ChangeLevel();
            fadeScript.transition = true;
        }
    }
}

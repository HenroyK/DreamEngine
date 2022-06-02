using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelPoint : MonoBehaviour
{
    private EndLevel endLevelScript;

    // Get a reference of EndLevel script attached to the scene game controller script
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            endLevelScript = gameController.GetComponent<EndLevel>();
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
            endLevelScript.EndLevelReached();
        }
    }
}

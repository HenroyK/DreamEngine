using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundary : MonoBehaviour
{
    private GameOver endScreenScript;
    
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            endScreenScript = gameController.GetComponent<GameOver>();
        }
        else
        {
            Debug.Log("Error. Couldn't find Game Controller");
        }
    }    

    // Checks if the player is the object to trigger the method
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endScreenScript.playerDied();
        }
    }
}

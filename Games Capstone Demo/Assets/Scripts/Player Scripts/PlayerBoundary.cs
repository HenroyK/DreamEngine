using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundary : MonoBehaviour
{
    //private GameOver endScreenScript;
    private LivesScript livesScript;
    
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            //endScreenScript = gameController.GetComponent<GameOver>();
            livesScript = gameController.GetComponent<LivesScript>();
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
            livesScript.LifeCountLoss(1);
        }
    }
}

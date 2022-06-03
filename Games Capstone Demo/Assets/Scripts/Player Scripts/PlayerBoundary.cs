using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundary : MonoBehaviour
{
    private LivesScript livesScript;
    
    // Get a reference of Lives Script that is attached to the Game controller object
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            livesScript = gameController.GetComponent<LivesScript>();
        }
        else
        {
            Debug.Log("Error. Couldn't find Game Controller");
        }
    }    

    // Subract one life if player collides with attached object
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            livesScript.LifeCountLoss(1);
        }
    }
}

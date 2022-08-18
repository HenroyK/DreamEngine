using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundary : MonoBehaviour
{
	public float damage = 1;

	private LifesScript livesScript;
	private GameContollerScript gameControllerScript;
	private DropInRespawn respawnScript;
	private GameObject player;
	private GameObject gameController;
	private BlackFade fader;

	// Get a reference of Lives Script that is attached to the Game controller object
	void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
	    fader = gameController.GetComponent<BlackFade>();
		player = GameObject.FindWithTag("Player");

		if (gameController != null)
        {
            livesScript = gameController.GetComponent<LifesScript>();
			gameControllerScript = gameController.GetComponent<GameContollerScript>();

		}
        else
        {
            Debug.Log("Error. Couldn't find Game Controller");
        }
    }

	void Update()
	{
		if (player == null)
		{
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                respawnScript = player.GetComponent<DropInRespawn>();
            }
            else
            {
                Debug.Log("Error. Couldn't find Player character");
            }
        }
		else
		{
			//Find the distance between player and the boundry
			float dist = Vector3.Distance(this.transform.position, player.transform.position);
			//Set the amount of fade
			if (dist < 25)
			{
				fader.SetFade((((dist / 25) - 1) * -1) * 100); //Quick maff
			}
			else
			{
				fader.SetFade(0);
			}
		}
	}

    // Subract one life if player collides with attached object
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (livesScript.enabled)
            {
				livesScript.LifeCountLoss(damage);
			}
			else
            {
                // Livesless respawn
                respawnScript.AltRespawnPlayer();
                //respawnScript.RespawnPlayer();
                //gameControllerScript.LoadCheckpoint();
            }
		}
    }
}

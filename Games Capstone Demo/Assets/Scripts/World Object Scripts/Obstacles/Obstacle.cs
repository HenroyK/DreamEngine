using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ObstacleScript
{
	[SerializeField]
	public List<ValidTags> hitTags;
	[SerializeField]
	public float damage;
	[SerializeField]
	public bool lifeLoss;
	[SerializeField]
	public bool destroyOnHit;

	private LifesScript livesScript;
	private DropInRespawn respawnScript;
	private GameObject player;
	private GameObject gameController;
	
	void Start()
	{
		gameController = GameObject.FindWithTag("GameController");
		player = GameObject.FindWithTag("Player");
		if(player != null)
			respawnScript = player.GetComponent<DropInRespawn>();
		else
			Debug.Log("Error. Couldn't find player");

		if (gameController != null)
		{
			livesScript = gameController.GetComponent<LifesScript>();
		}
		else
		{
			Debug.Log("Error. Couldn't find Game Controller");
		}
	}

    private void Update()
    {
		//Make sure these aint null fam
		if (player == null)
			player = GameObject.FindWithTag("Player");
		if(respawnScript == null)
			respawnScript = player.GetComponent<DropInRespawn>();
		if (gameController == null)
			gameController = GameObject.FindWithTag("GameController");
	}

    //Do things when colliding with something
    public override void Collided(Collider hit)
	{
		//Hit Player
		if(hit.tag == "Player" && hitTags.Contains(ValidTags.Player))
		{
			print("hit player");
			if(lifeLoss)
			{
				if(livesScript.enabled)
					livesScript.LifeCountLoss(1);
				else
					respawnScript.RespawnPlayer();

			}
			else if (damage != 0)
			{
				//Life loss without reset and/or health damage?
				//hit.gameObject.GetComponent<LivesScript>().LifeCountLoss(1);
			}

		}

		if(destroyOnHit)
			Destroy(gameObject);
	}
}

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
	public bool DestroyOnHit;

	//Do things when colliding with something
	public override void Collided(Collider hit)
	{
		//Hit Player
		if(hit.tag == "Player" && hitTags.Contains(ValidTags.Player))
		{
			print("hit player");
			if(lifeLoss)
			{
				GameObject.FindGameObjectWithTag("GameController").GetComponent<LivesScript>().LifeCountLoss(1);
			}
			else if (damage != 0)
			{
				//Life loss without reset and/or health damage?
				//hit.gameObject.GetComponent<LivesScript>().LifeCountLoss(1);
			}

		}

		if(DestroyOnHit)
			Destroy(gameObject);
	}
}

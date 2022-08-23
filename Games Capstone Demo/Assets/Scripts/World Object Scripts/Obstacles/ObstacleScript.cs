using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleScript : MonoBehaviour
{
	//Tags that have functionality when colliding with
	public enum ValidTags
	{
		Player
	}
	private void OnTriggerEnter(Collider other)
	{
		Collided(other);
	}

	public abstract void Collided(Collider hit);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour
{
	//Vars
	public Image blackFader;
	public float maxFade;
	public float fadeDist;

	private GameObject closest;
	private float closeDist = 25;
	private GameObject player;
	private RaycastHit hit;

	public void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

	public void Update()
	{

		Physics.Raycast(player.transform.position, (player.transform.position - closest.transform.position).normalized,
			out hit, 25, 1);
		if (hit.collider.gameObject == closest)
		{

		}
		else
		{
			closest = null;
			closeDist = 25;
		}
	}

	//Set the fade 0-100% of maxFade
	public void SetFade(GameObject obj, float dist)
	{
		//Listen to only the closest object for the fade
		if (closest != obj && dist <= 25 && dist < closeDist)
		{
			closest = obj;
			closeDist = dist;
		}
		if (closest == obj && closeDist <= 25)
		{
			float percent = Mathf.InverseLerp(
				0, 255, (((((dist / 25) - 1) * -1) * 100) / 100) * maxFade); //Quick Maff
			blackFader.color = new Color(0, 0, 0, percent);
		}
	}

	//Reset the fade amount
	public void ResetFade()
	{
		blackFader.color = new Color(0, 0, 0, 0);
	}

	public void FadeAmount(float amount)
	{
		float percent = Mathf.InverseLerp(
			0, 255, (((((amount / 25) - 1) * -1) * 100) / 100) * maxFade); //Quick Maff
		blackFader.color = new Color(0, 0, 0, percent);
	}
}

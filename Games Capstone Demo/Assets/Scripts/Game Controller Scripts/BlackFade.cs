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

	//Set the fade 0-100% of maxFade
	public void SetFade(GameObject obj,float dist)
	{
		//Listen to only the closest object for the fade
		if (closest != obj && dist <= 25 && dist < closeDist)
		{
			closest = obj;
			closeDist = dist;
		}
		if (closest == obj && closeDist <= 25)
		{
			float percent = Mathf.InverseLerp(0, 255, (((((dist / 25) - 1) * -1) * 100) / 100) * maxFade); //Quick Maff
			blackFader.color = new Color(0, 0, 0, percent);

			print(closest);
			print(closeDist);
		}
	}

	//Reset the fade amount
	public void ResetFade()
	{
		blackFader.color = new Color(0, 0, 0, 0);
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour
{
	//Vars
	public Image blackFader;
	public float maxFade;

	//Set the fade 0-100% of maxFade
	public void SetFade(float percent)
	{
		percent = Mathf.InverseLerp(0,255,(percent/100)*maxFade);
		blackFader.color = new Color(0, 0, 0, percent);
	}
	
}

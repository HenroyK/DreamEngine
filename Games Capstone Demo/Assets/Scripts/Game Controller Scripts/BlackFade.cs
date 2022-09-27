using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour
{
	//Vars
	public Image blackFader;
    public Image transitionFader;
	public float maxFade;
	public float fadeDist;
    public float transitionTime;
    public bool transition;

    private GameObject closest;
	private float closeDist = 25;
	private GameObject player;
	private RaycastHit hit;
    private float fadeTimer;

	public void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

	public void LateUpdate()
	{
        //Fade transition
        if(transition)
        {
            if (fadeTimer < 1)
            {
                print(fadeTimer);
                fadeTimer += Time.deltaTime / transitionTime;
                transitionFader.color = new Color(0, 0, 0, fadeTimer);
            }
            else
            {
                Debug.Log("Change level");
                BroadcastMessage("ChangeLevel");  // Calls function with this name for any script
                transition = false;
            }

        }

		//Raycast to get the true distance to the closest object (so size is accounted for)
		if (player != null)
		{
			if (closest != null)
			{
				Debug.DrawRay(player.transform.position, -player.transform.right * 25);
				//Close to a boundary
				Physics.Raycast(player.transform.position, -player.transform.right,
					out hit, fadeDist, LayerMask.GetMask("Boundary"), UnityEngine.QueryTriggerInteraction.Collide);
				if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Boundary"))
				{
					closeDist = Vector3.Distance(player.transform.position, hit.point);
					FadeAmount(closeDist);
				}
				else
				{
					closest = null;
					closeDist = 25;
				}
				//Close to other fade triggers
				/*
				Physics.Raycast(player.transform.position, closest.transform.position-player.transform.position,
					out hit, fadeDist);
				if (hit.collider)
				{
					closeDist = Vector3.Distance(player.transform.position, hit.point);
					FadeAmount(closeDist);
				}
				else
				{
					closest = null;
					closeDist = 25;
				}
				*/
			}
		}
		else
		{
			player = GameObject.FindWithTag("Player");
		}
	}

	//Set the fade 0-100% of maxFade
	public void SetFade(GameObject obj, float dist)
	{
		//Listen to only the closest object for the fade
		if (closest != obj && dist <= fadeDist && dist < closeDist)
		{
			closest = obj;
			closeDist = dist;
		}
		if (closest == obj && closeDist <= fadeDist)
		{
			FadeAmount(dist);
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
			0, 255, (((((amount / fadeDist) - 1) * -1) * 100) / 100) * maxFade); //Quick Maff
		blackFader.color = new Color(0, 0, 0, percent);
	}
}

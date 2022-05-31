using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
	//Variables
	///public Object gameScene; This currently doesnt work when building, comes up as null
	public List<Texture2D> introImages;
	public GameObject cutsceneUI;
	public AudioSource audioSource;
	public AudioClip cutsceneMusic;
	private int curScene = -1;

	//Startup stuff
	void Start()
    {
		audioSource.Play(0);
    }

	// Update is called once per frame
	void Update()
	{
		//Progress when jump is pressed, loading when hitting the end of the images
		if (Input.GetButtonDown("Jump") && curScene >= 0)
		{
			curScene++;
			if (curScene >= introImages.Count)
			{
				curScene = -1;
				StartCoroutine(LoadAsyncScene());
			}
			else
				cutsceneUI.GetComponent<RawImage>().texture = introImages[curScene];
		}
	}

	//Button pressed
	public void OnPButtonPress()
	{
		curScene = 0;
		audioSource.clip = cutsceneMusic;
		audioSource.Play(0);
		cutsceneUI.SetActive(true);
	}

	//Button pressed
	public void OnQButtonPress()
	{
		Application.Quit();
	}

	//Load scene (asynchronous)
	IEnumerator LoadAsyncScene()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

		//Wait until scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}

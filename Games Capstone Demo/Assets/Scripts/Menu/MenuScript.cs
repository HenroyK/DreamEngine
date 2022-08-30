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
	private int numberOfOptions = 2;
	private int selectedOption;
	private bool inMenu = true;

	//Startup stuff
	void Start()
    {
		audioSource.Play(0);

		selectedOption = 1;
	}

	// Update is called once per frame
	void Update()
	{
		if (inMenu)
        {
			// select options code
			if (Input.GetButtonDown("SwapForward") /*|| Input.GetAxis("SwapForward") > 0*/)
			{
				selectedOption += 1;
				if (selectedOption > numberOfOptions)
				{
					selectedOption = 1;
				}

				// reset selected highlight
				SwapSelected(selectedOption);
			}

			if (Input.GetButtonDown("SwapBackward") /*|| Input.GetAxis("SwapBackward") > 0*/)
			{
				selectedOption -= 1;
				if (selectedOption < 1)
				{
					selectedOption = numberOfOptions;
				}

				// reset selected highlight
				SwapSelected(selectedOption);
			}

			if (Input.GetButton("Jump"))
			{

				switch (selectedOption)
				{
					case 1:
						OnPButtonPress();
						break;
					case 2:
						OnQButtonPress();
						break;
				}
			}
		}

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
		if (Input.GetButtonDown("Skip"))
		{
			curScene = -1;
			StartCoroutine(LoadAsyncScene());
		}
	}

	void SwapSelected(int option)
	{
		// reset selected highlight

		Debug.Log("Picked: " + selectedOption);
		switch (selectedOption)
		{
			case 1:
				/*Do option two*/
				break;
			case 2:
				/*Do option two*/
				break;
		}
	}

	//Button pressed
	public void OnPButtonPress()
	{
		curScene = 0;
		audioSource.clip = cutsceneMusic;
		audioSource.Play(0);
		cutsceneUI.SetActive(true);
		inMenu = false;
	}

	//Button pressed
	public void OnQButtonPress()
	{
		inMenu = false;
		Application.Quit();
	}

	//Load scene (asynchronous)
	IEnumerator LoadAsyncScene()
	{
		AsyncOperation asyncLoad = 
			SceneManager.LoadSceneAsync(1);

		//Wait until scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}

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
    public Button playBtn;
    public Button quitBtn;
    public GameObject btnHighlight;

    private int curScene = -1;
	private int numberOfOptions = 2;
	private int selectedOption;
	private bool inMenu = true;

	private float inputTimer = 0;
	[SerializeField]
	private float waitTime = 0.3f;

	//Startup stuff
	void Start()
    {
		audioSource.Play(0);

		selectedOption = 1;
        btnHighlight.transform.position =
                    playBtn.transform.position;
        btnHighlight.SetActive(true);
    }

    // Update is called once per frame
    void Update()
	{
		inputTimer += Time.unscaledDeltaTime;

		if (inMenu && inputTimer >= waitTime)
        {
			// select options code
			if (Input.GetAxisRaw("Swap") > 0)
			{
				selectedOption += 1;
				if (selectedOption > numberOfOptions)
				{
					selectedOption = 1;
				}

				// reset selected highlight
				SwapSelected(selectedOption);
			}

			if (Input.GetAxisRaw("Swap") < 0)
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
                btnHighlight.transform.position =
                    playBtn.transform.position;
                break;
			case 2:
                btnHighlight.transform.position =
                    quitBtn.transform.position;
                break;
		}
		inputTimer = 0;
	}

	//Button pressed
	public void OnPButtonPress()
	{
		curScene = 0;
		audioSource.clip = cutsceneMusic;
		audioSource.Play(0);
		cutsceneUI.SetActive(true);
		inMenu = false;
        btnHighlight.SetActive(false);
    }

	//Button pressed
	public void OnQButtonPress()
	{
		inMenu = false;
        btnHighlight.SetActive(false);
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

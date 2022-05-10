using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
	//Variables
	public SceneAsset gameScene;
	public List<Texture2D> introImages;
	public GameObject cutsceneUI;
	private int curScene = -1;

	// Update is called once per frame
	void Update()
	{
		//Progress when jump is pressed, loading when hitting the end of the images
		if (Input.GetButtonDown("Jump") && curScene > 0)
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
	public void OnButtonPress()
	{
		curScene = 1;
		cutsceneUI.SetActive(true);
	}

	//Load scene (asynchronous)
	IEnumerator LoadAsyncScene()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameScene.name);

		//Wait until scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}

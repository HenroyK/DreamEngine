using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreSceneScript : MonoBehaviour
{
    public Image transitionFader;
    public GameObject loadingThrobber;

    private float fadeTimer = 1;
    private bool loaded = false;

    public int nextSceneNum = -1;
    //Startup stuff
    void Start()
    {
        transitionFader.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Progress when jump is pressed, loading when hitting the end of the images
        if (Input.GetButtonDown("Jump") || Input.GetButton("Enter"))
        {
            LoadSceneOnClick();
        }

        if (loaded)
        {
            fadeTimer += Time.deltaTime;
            transitionFader.color = new Color(0, 0, 0, fadeTimer);
        }
        else if (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            transitionFader.color = new Color(0, 0, 0, fadeTimer);
        }
    }

    public void LoadSceneOnClick()
    {
        if (nextSceneNum != -1)
        {
            loadingThrobber.SetActive(true);
            StartCoroutine(LoadAsyncScene());
        }
    }

    //Load scene (asynchronous)
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad =
            SceneManager.LoadSceneAsync(nextSceneNum);
        asyncLoad.allowSceneActivation = false;

        //Wait until scene fully loads
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                if (asyncLoad.allowSceneActivation == false)
                {
                    loaded = true;
                }
            }
            //Allow loading when faded
            if (transitionFader.color.a >= 1)
                asyncLoad.allowSceneActivation = true;
            yield return null;
        }

        //AsyncOperation asyncLoad =
        //    SceneManager.LoadSceneAsync(nextSceneNum);

        ////Wait until scene fully loads
        //while (!asyncLoad.isDone)
        //{
        //    yield return null;
        //}
    }
}

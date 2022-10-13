using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreSceneScript : MonoBehaviour
{
    public Image transitionFader;
    public GameObject loadingNum;
    public GameObject loadingText;
    public bool canGoNext = false;
    private float fadeTimer = 1;
    private bool loaded = false;
    private bool triedSkip = false;
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
        if(canGoNext)
        {
            if (nextSceneNum != -1)
            {
                StartCoroutine(LoadAsyncScene());
            }
        }
        else
        {
            ScoreSkip();
        }
       
    }

    //Load scene (asynchronous)
    IEnumerator LoadAsyncScene()
    {
        loadingText.gameObject.SetActive(true);
        AsyncOperation asyncLoad =
            SceneManager.LoadSceneAsync(nextSceneNum);
        asyncLoad.allowSceneActivation = false;

        //Wait until scene fully loads
        while (!asyncLoad.isDone)
        {
            //Whatever happened to just getting Text.text? seriously this is dumb
            loadingNum.GetComponent<TextMeshProUGUI>().GetComponent<TMP_Text>().text = Mathf.Round((asyncLoad.progress * 100)) + "%";
            loadingNum.gameObject.transform.Find("LoadingNum").GetComponent<TextMeshProUGUI>().GetComponent<TMP_Text>().text =
                loadingNum.GetComponent<TextMeshProUGUI>().GetComponent<TMP_Text>().text;
            if (asyncLoad.progress >= 0.9f)
            {
                loadingNum.gameObject.SetActive(false);
                loadingText.GetComponent<TextMeshProUGUI>().GetComponent<TMP_Text>().text = "Done!";
                loadingText.gameObject.transform.Find("LoadingText").GetComponent<TextMeshProUGUI>().GetComponent<TMP_Text>().text = "Done!";
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

    void ScoreSkip()
    {
        if (!triedSkip)
        {
            BroadcastMessage("SkipScore");
            StartCoroutine(WaitSkip(0.5f));
            triedSkip = true;
        }
    }
    IEnumerator WaitSkip(float pTime)
    {
        yield return new WaitForSecondsRealtime(pTime);
        canGoNext = true;
    }

}

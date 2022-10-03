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

    private float fadeTimer = 0;
    private bool loaded = false;

    public int nextSceneNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (loaded)
        {
            fadeTimer += Time.deltaTime;
            transitionFader.color = new Color(0, 0, 0, fadeTimer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Progress when jump is pressed, loading when hitting the end of the images
        if (Input.GetButtonDown("Jump") || Input.GetButton("Enter"))
        {
            LoadSceneOnClick();
        }
    }

    public void LoadSceneOnClick()
    {
        if (nextSceneNum != -1)
        {
            StartCoroutine(LoadAsyncScene());
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
}

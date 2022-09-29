using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CutSceneScript : MonoBehaviour
{
    //Variables
    ///public Object gameScene; This currently doesnt work when building, comes up as null
    public List<Texture2D> introImages;
    public GameObject cutsceneUI;
    public AudioSource audioSource;
    public AudioClip cutsceneMusic;
    public AudioClip pickClip;
    public int nextSceneNum = -1;

    private int curScene = -1;

    //Startup stuff
    void Start()
    {
        audioSource.Play(0);

        curScene = 0;
        audioSource.clip = cutsceneMusic;
        audioSource.Play(0);
        cutsceneUI.SetActive(true);
        cutsceneUI.GetComponent<RawImage>().texture = introImages[curScene];
    }

    // Update is called once per frame
    void Update()
    {
        ProceedScene();

        if (curScene > 0)
        {
            cutsceneUI.gameObject.transform.Find("ProgressText").gameObject.SetActive(false);
            cutsceneUI.gameObject.transform.Find("SkipText").gameObject.SetActive(false);
        }
    }

    void ProceedScene()
    {
        if (Input.GetButtonDown("Jump") && curScene >= 0)
        {
            curScene++;
            if (curScene >= introImages.Count)
            {
                curScene = -1;
                if (nextSceneNum != -1)
                {
                    StartCoroutine(LoadAsyncScene());
                }
            }
            else
                cutsceneUI.GetComponent<RawImage>().texture = introImages[curScene];
        }
        if (Input.GetButtonDown("Dash"))
        {
            curScene = -1;
            if (nextSceneNum != -1)
            {
                StartCoroutine(LoadAsyncScene());
            }
        }
    }

    //Load scene (asynchronous)
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad =
            SceneManager.LoadSceneAsync(nextSceneNum);

        //Wait until scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

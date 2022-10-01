using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreSceneScript : MonoBehaviour
{
    public int nextSceneNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        
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

        AsyncOperation asyncLoad =
            SceneManager.LoadSceneAsync(nextSceneNum);

        //Wait until scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

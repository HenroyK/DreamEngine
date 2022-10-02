using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;

public class EndLevel : MonoBehaviour
{
    public GameObject endLevelMenuUI;
    public GameObject nextLevelBtn;
    public Button reloadLevelBtn;
    public Button mainMenuBtn;
    public GameObject btnHighlight;
    public int nextSceneNum = -1;
    public bool menuEnabled = false;

    public Image transitionFader;
    public GameObject loadingNum;
    public GameObject loadingText;

    private float fadeTimer = 0;
    private bool loaded = false;

    private GameContollerScript gameControllerScript;
    private Pause pauseScript;
    private ScoreScript scoreScript;
    private BlackFade fadeScript;

    private List<HighScoreEntry> leaderboard = new List<HighScoreEntry>();

    // button select varaibles
    private int numberOfOptions = 3; 
    private int selectedOption;
    private bool gameEnded = false;

    private float inputTimer = 0;
    [SerializeField]
    private float waitTime = 0.5f;

    // Set up variables, find scripts, and enable buttons
    void Start()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");

        if (gameController != null)
        {
            pauseScript = gameController.GetComponent<Pause>();
            gameControllerScript = gameController.GetComponent<GameContollerScript>();
            scoreScript = gameController.GetComponent<ScoreScript>();
            fadeScript = gameController.GetComponent<BlackFade>();
        }
        else
        {
            Debug.Log("Error. Couldn't find Game Controller");
        }

        Button btnRetry = reloadLevelBtn.GetComponent<Button>();
        btnRetry.onClick.AddListener(ReloadOnClick);

        Button btnMainMenu = mainMenuBtn.GetComponent<Button>();
        btnMainMenu.onClick.AddListener(MainMenuOnClick);

        endLevelMenuUI.SetActive(false);

        selectedOption = 1;
        // position selected highlight Icon/shade
        btnHighlight.transform.position =
                    reloadLevelBtn.transform.position;
        btnHighlight.SetActive(false);
    }

    void Update()
    {
        if (loaded)
        {
            fadeTimer += Time.deltaTime;
            transitionFader.color = new Color(0, 0, 0, fadeTimer);
        }

        if (gameEnded)
        {
            inputTimer += Time.unscaledDeltaTime;

            if (inputTimer >= waitTime)
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

                if (Input.GetButton("Jump") ||
                    Input.GetButton("Enter"))
                {
                    switch (selectedOption)
                    {
                        case 1:
                           NextLevelOnClick();
                            break;
                        case 2:
                            ReloadOnClick();
                            break;
                        case 3:
                            MainMenuOnClick();
                            break;
                    }
                }
            }
        }
    }

    // still testing this function
    //public IEnumerator FadeBlackOut(bool fadeToBlack = true, int fadeSpeed = 5)
    //{
    //    Color objectColor = blackOutSquare.GetComponent<Image>().color;
    //    float fadeAmount;

    //    if (fadeToBlack)
    //    {
    //        while (blackOutSquare.GetComponent<Image>().color.a < 1)
    //        {
    //            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

    //            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //            blackOutSquare.GetComponent<Image>().color = objectColor;
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        while (blackOutSquare.GetComponent<Image>().color.a > 0)
    //        {
    //            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

    //            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //            blackOutSquare.GetComponent<Image>().color = objectColor;
    //            yield return null;
    //        }
    //    }
        
    //    yield return new WaitForEndOfFrame();
    //}

    void SwapSelected(int option)
    {
        // reset selected highlight
        inputTimer = 0;
        Debug.Log("Picked: " + selectedOption);
        if (btnHighlight != null)
        {
            switch (selectedOption)
            {
                case 1:
                    btnHighlight.transform.position = 
                        nextLevelBtn.transform.position;
                    break;
                case 2:
                    btnHighlight.transform.position =
                        reloadLevelBtn.transform.position;
                    break;
                case 3:
                    btnHighlight.transform.position =
                        mainMenuBtn.transform.position;
                    break;
            }
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
            {
                asyncLoad.allowSceneActivation = true;
                fadeScript.loading = true;
            }
            yield return null;
        }

        ////////

        //AsyncOperation asyncLoad =
        //    SceneManager.LoadSceneAsync(nextSceneNum);

        ////Wait until scene fully loads
        //while (!asyncLoad.isDone)
        //{
        //    yield return null;
        //}
    }

    void NextLevelOnClick()
    {
        if (nextSceneNum != -1)
        {
            StartCoroutine(LoadAsyncScene());
        }
    }

    // Reload on button press
    void ReloadOnClick()
    {
        Reload();
    }

    // Load Main Menu scene when menu button press
    void MainMenuOnClick()
    {
        SceneManager.LoadScene(0);
    }

    // Reloads the current scene
    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Turns on end game UI, pauses game and music
    public void ChangeLevel()
    {
        if (menuEnabled)
        {
            pauseScript.disablePause(); // disable pause functionality
            gameControllerScript.PlayerControls(false); // disable player controls
            endLevelMenuUI.SetActive(true); // enable game over UI
            gameEnded = true; // game has ended
                              // pause game music (attached to game controller object)
            gameObject.GetComponent<AudioSource>().Pause();
            btnHighlight.SetActive(true);
            Time.timeScale = 0; // pause game

            //DisplayLeaderboard();
            //WriteLeaderboard();
        }
        else
        {
            NextLevelOnClick();
        }
    }
    //private void DisplayLeaderboard()
    //{
    //    leaderboard = XMLManager.instance.LoadScores(1);
    //    leaderboard.Add(new HighScoreEntry { name = "Name Not Implemented + " + scoreScript.getScore() , score = scoreScript.getScore() });
    //    leaderboard.OrderBy(HighScoreEntry => HighScoreEntry.score);

    //    //Change this for an actual display
    //    Debug.Log("----Printing Scoreboard----");
    //    int i = 1;
    //    foreach (HighScoreEntry h in leaderboard)
    //    {
    //        Debug.Log(i +". "+ h.name + ": "+ h.score);
    //    }
    //    Debug.Log("----Finished Printing----");
    //}
    //private void WriteLeaderboard()
    //{
    //    XMLManager.instance.SaveScores(leaderboard, 1);
    //}

    // Mouse over Retry button
    public void MONextLevelBtn()
    {
        selectedOption = 1;
        SwapSelected(selectedOption);
    }

    public void MORetryBtn()
    {
        selectedOption = 2;
        SwapSelected(selectedOption);
    }

    // Mouse over Menu button
    public void MOMenuBtn()
    {
        selectedOption = 3;
        SwapSelected(selectedOption);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text fpsCounter;
    public GameObject fpsCounterObject;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        fpsCounter = fpsCounterObject.GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tilde))
        {
            PlayerStats.showFPSCounter = !PlayerStats.showFPSCounter;
        }
        float fps = (1f / Time.unscaledDeltaTime);
        if (fps < 60)
        {
            Debug.LogWarning("Low FPS" + fps);
        }
        if (PlayerStats.showFPSCounter)
        {
            fpsCounter.text = ((int)fps).ToString();
        }
        else
        {
            fpsCounter.text = "";
        }
    }
}

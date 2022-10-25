using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text fpsCounter;
    // Start is called before the first frame update
    void Start()
    {
        fpsCounter = this.gameObject.GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
       fpsCounter.text = ((int)(1f / Time.unscaledDeltaTime)).ToString();
    }
}

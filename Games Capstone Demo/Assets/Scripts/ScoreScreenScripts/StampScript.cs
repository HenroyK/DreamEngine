using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampScript : MonoBehaviour
{
    public float startScale = 20;
    public float endScale = 2;
    public float timeToScale = 1;
    private float elapsedTime = 0;
    private bool startScaling = false;
    private float scale = 1;
    private void OnEnable()
    { 

        scale = Mathf.Lerp(startScale,endScale, 0);
        startScaling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startScaling)
        {
            elapsedTime += Time.deltaTime;
            scale = Mathf.Lerp(startScale, endScale, Mathf.Clamp(elapsedTime / timeToScale, 0, 1));
            this.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}

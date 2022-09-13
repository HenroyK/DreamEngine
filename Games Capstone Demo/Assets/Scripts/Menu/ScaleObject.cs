using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    //Variables
    public GameObject scaledObject;
    private Resolution targetRes;
    private Resolution currentRes;

    // Start is called before the first frame update
    void Start()
    {
        targetRes = Screen.currentResolution;
        print(targetRes.height + "|" + targetRes.width);
        Scale(targetRes);
    }

    // Update is called once per frame
    void Update()
    {
        currentRes = Screen.currentResolution;
        //Check for resolution change, update accordingly
        if(targetRes.width != currentRes.width || targetRes.height != currentRes.height)
        {
            targetRes = currentRes;
            Scale(targetRes);
        }
    }

    //Scale the object
    void Scale(Resolution scale)
    {
        float width = scale.width / 1200f;
        float height = scale.height / 1000f;
        print(height + "|" + width);
        scaledObject.transform.localScale = new Vector3(width, height, scaledObject.transform.localScale.z);
    }
}

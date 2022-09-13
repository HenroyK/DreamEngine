using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    //Variables
    public GameObject scaledObject;
    public Vector2 targetRes;
    private Vector2 currentRes;

    // Start is called before the first frame update
    void Start()
    {
        currentRes = new Vector2 (Screen.currentResolution.width, Screen.currentResolution.height);
        print(currentRes.x + "|" + currentRes.y);
        Scale();
    }

    //Scale the object
    void Scale()
    {
        Vector2 diff = currentRes - targetRes;
        print(diff.x + "|" + diff.y);
        scaledObject.transform.localScale += (scaledObject.transform.localScale * (((diff.x / targetRes.x)*100)/100));
    }
}

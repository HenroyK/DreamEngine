using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Variables
    public Camera camera;
    public GameObject lookatObject;

    private bool curState;
    private float tilt;
    private Transform camTransform;

    // Start is called before the first frame update
    void Start()
    {
        curState = true;
        camTransform = camera.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check state
        if(curState & lookatObject)
        {
            tilt = lookatObject.GetComponent<Transform>().position.x/3;
            tilt = Mathf.Clamp(tilt,-20,17);
            camTransform.localEulerAngles = new Vector3(0,tilt,0);
        }
        else
        {
            camTransform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    // Public function for enable / disabling
    public void EnableTilt(bool newState)
    {
        curState = newState;
    }

    // Public function for what to look at
    public void SetLookat(GameObject newLookat)
    {
        lookatObject = newLookat;
    }
}

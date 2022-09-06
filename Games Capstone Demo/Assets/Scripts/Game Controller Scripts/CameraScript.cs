using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Variables
    private Camera mainCamera;
    private GameObject lookatObject;

    private bool curState;
    private float tilt;
    private Transform camTransform;

    // Start is called before the first frame update
    void Start()
    {
        GameObject theCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
        if (theCamera != null)
        {
            mainCamera = theCamera.GetComponent<Camera>();
            curState = true;
            camTransform = mainCamera.GetComponent<Transform>();
        }
        else
        {
            Debug.LogError("Couldn't find Main Cameara");
        }
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

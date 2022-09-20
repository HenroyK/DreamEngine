using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Variables
    public float forwardClamp;
    public float backwardClamp;
    public float upClamp;
    public float downClamp;
    private Camera mainCamera;
    private GameObject lookatObject;

    private bool curState;
    private float yaw;
    private float pitch;
    private Transform camTransform;
    private Vector3 startRot;
    // Start is called before the first frame update
    void Start()
    {
        GameObject theCamera = GameObject.FindGameObjectWithTag("MainCamera");
        startRot = transform.rotation.eulerAngles;
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
            yaw = lookatObject.GetComponent<Transform>().position.x/3;
            yaw = Mathf.Clamp(yaw,backwardClamp,forwardClamp);
            pitch = lookatObject.GetComponent<Transform>().position.y / 5;
            pitch = Mathf.Clamp(pitch, upClamp, downClamp);
            camTransform.localEulerAngles = new Vector3(-pitch,yaw, camTransform.rotation.eulerAngles.z);
        }
        else
        {
            camTransform.localEulerAngles = startRot;
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

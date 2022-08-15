using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDestinationScript : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cameraObject = GameObject.FindWithTag("MainCamera");

        if (cameraObject != null)
        {
            sceneCamera = cameraObject.GetComponent<Camera>();
        }
        else
        {
            Debug.Log("Couldn't find player camera.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = sceneCamera.ScreenToWorldPoint(new Vector3(1600, 900, 20));
    }
}

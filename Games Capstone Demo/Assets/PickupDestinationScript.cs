using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDestinationScript : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = sceneCamera.ScreenToWorldPoint(new Vector3(1600, 900, 20));
    }
}

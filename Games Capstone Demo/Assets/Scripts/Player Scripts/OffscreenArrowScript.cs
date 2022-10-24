using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OffscreenArrowScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Camera mainCamera;
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(player.transform.position.y < 30)
        {
            this.transform.position = new Vector3(-100, -100, 0); 
        }
        else
        {
            Vector3 playerScreenPos = mainCamera.WorldToViewportPoint(player.transform.position);
            this.transform.position = new Vector3(Screen.width * playerScreenPos.x, Screen.height*0.83f, 0);
        }
    }
}

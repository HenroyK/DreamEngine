using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpotlight : MonoBehaviour
{
    public float speed = 10;
    
    private GameObject playerCharacter;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerCharacter = player;
        }
        else
        {
            Debug.Log("Couldn't find player character");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        Vector3 playerPosition = playerCharacter.transform.position;
        Vector3 targetPostion = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);

        if (Vector3.Distance(transform.position, targetPostion) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPostion, step);
        }
    }
}

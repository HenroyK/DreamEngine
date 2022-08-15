using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInRespawn : MonoBehaviour
{
    public float spawnXAxis = 0;
    public float spawnYAxis = 30;

    public Vector2 respawnPostionA = new Vector2(0, 30);

    private GameObject playerCharacter;
    private DepthBehaviour depthScript;
    private bool[] layerActive;
    private float[] layerZAxis;
    private int curLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.FindWithTag("Player");

        if (playerCharacter != null)
        {
            depthScript = playerCharacter.GetComponent<DepthBehaviour>();
        }
        else
        {
            Debug.Log("Error. Couldn't find Player object.");
        }

        layerActive = depthScript.layerAvailable;
        layerZAxis = depthScript.layerAxis;
        curLayer = depthScript.curDepth;
    }

    public void RespawnPlayer()
    {
        // currently only respawns the player on the layer they are currently on

        curLayer = depthScript.curDepth;

        if (layerActive[curLayer])
        {
            playerCharacter.transform.position = new Vector3(
                respawnPostionA.x, respawnPostionA.y, layerZAxis[curLayer]);
        }
        else
        {
            // alternate layer/position
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

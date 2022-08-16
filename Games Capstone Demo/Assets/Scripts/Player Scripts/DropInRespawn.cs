using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInRespawn : MonoBehaviour
{
    public float spawnXAxis = 0;
    public float spawnYAxis = 30;

    public Vector2 respawnPostionA = new Vector2(0, 30);
    public Vector2 respawnPostionB = new Vector2(10, 30);
    public Vector2 respawnPostionC = new Vector2(20, 30);

    private GameObject playerCharacter;
    private DepthBehaviour depthScript;
    private bool[] layerActive;
    private float[] layerZAxis;
    private int curLayer;

    private bool blockDetect = false;
    private RaycastHit blockRaycastHit;

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

    // function needs further work
    // Advanced respawn function
    // 1. checks current layer to see if any spawn positions are free
    // 2. checks the the forward most layers relative to the current layer
    // 3. brute force search all layers to find an open position
    // 4. default respawn position
    public void AltRespawnPlayer()
    {
        // currently only respawns the player on the layer they are currently on
        curLayer = depthScript.curDepth;

        // needs to be an array
        Vector3 spawnPoint1 = new Vector3(
                respawnPostionA.x, respawnPostionA.y, layerZAxis[curLayer]);
        Vector3 spawnPoint2 = new Vector3(
                respawnPostionB.x, respawnPostionB.y, layerZAxis[curLayer]);
        Vector3 spawnPoint3 = new Vector3(
                respawnPostionC.x, respawnPostionC.y, layerZAxis[curLayer]);

        LayerMask mask = LayerMask.GetMask(new string[] { "GroundFloor", "Building" });
        Quaternion weirdQuat = new Quaternion();
        weirdQuat.eulerAngles = new Vector3(0, 0, 0);
        if (!Physics.CheckBox(spawnPoint1, transform.localScale, weirdQuat, mask))
        {
            playerCharacter.transform.position = spawnPoint1;
        }
        else if (!Physics.CheckBox(spawnPoint2, transform.localScale, weirdQuat, mask))
        {
            playerCharacter.transform.position = spawnPoint2;
        }
        else if (!Physics.CheckBox(spawnPoint3, transform.localScale, weirdQuat, mask))
        {
            playerCharacter.transform.position = spawnPoint3;
        }
        else if (curLayer != 0)
        {
            int nextLayer = curLayer;

            while (nextLayer >= 0)
            {
                nextLayer -= 1;

                if (layerActive[nextLayer])
                {
                    spawnPoint1 = new Vector3(
                    respawnPostionA.x, respawnPostionA.y, layerZAxis[nextLayer]);
                    spawnPoint2 = new Vector3(
                        respawnPostionB.x, respawnPostionB.y, layerZAxis[nextLayer]);
                    spawnPoint3 = new Vector3(
                        respawnPostionC.x, respawnPostionC.y, layerZAxis[nextLayer]);

                    if (!Physics.CheckBox(spawnPoint1, transform.localScale, weirdQuat, mask))
                    {
                        playerCharacter.transform.position = spawnPoint1;
                        break;
                    }
                    else if (!Physics.CheckBox(spawnPoint2, transform.localScale, weirdQuat, mask))
                    {
                        playerCharacter.transform.position = spawnPoint2;
                        break;
                    }
                    else if (!Physics.CheckBox(spawnPoint3, transform.localScale, weirdQuat, mask))
                    {
                        playerCharacter.transform.position = spawnPoint3;
                        break;
                    }
                }

            }
        }
    }

    public void RespawnPlayer()
    {
        // currently only respawns the player on the layer they are currently on
        curLayer = depthScript.curDepth;

        // finds the closest active layer, assumes that the
        // first active layer in the list is the closest
        int index = 0;
        foreach (bool layer in layerActive)
        {
            if (layer)
            {
                break;
            }
            else
            {
                index++;
            }
        }

        if (layerActive[index])
        {
            depthScript.curDepth = index;
            playerCharacter.transform.position = new Vector3(
                respawnPostionA.x, respawnPostionA.y, layerZAxis[index]);
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

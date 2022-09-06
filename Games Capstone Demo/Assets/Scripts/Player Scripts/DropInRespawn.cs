using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class DropInRespawn : MonoBehaviour
{
    public Vector2 defaultRespawn = new Vector2(0, 30);
    
    public Vector2[] respawnPostions;

    private GameObject playerCharacter;
    private DepthBehaviour depthScript;
    private bool[] layerActive;
    private float[] layerZAxis;
    private int curLayer;
	private BlackFade fader;

    //private bool blockDetect = false;
    //private RaycastHit blockRaycastHit;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.FindWithTag("Player");
		fader = GameObject.FindWithTag("GameController").GetComponent<BlackFade>();

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

    // Advanced respawn function
    // 1. checks current layer to see if any spawn positions are free
    // 2. checks the forward most layers relative to the current layer
    // 3. brute force search all layers to find an open position
    // 4. default respawn position
    public void AltRespawnPlayer()
    {
        fader.ResetFade();

        // 1.checks current layer to see if any spawn positions are free
        curLayer = depthScript.curDepth;

        LayerMask mask = LayerMask.GetMask(new string[] { "GroundFloor", "Building" });
        Quaternion weirdQuat = new Quaternion();
        weirdQuat.eulerAngles = new Vector3(0, 0, 0);

        foreach (Vector2 xyPostion in respawnPostions)
        {
            Vector3 spawnPoint = new Vector3(
                xyPostion.x, xyPostion.y, layerZAxis[curLayer]);

            if (!Physics.CheckBox(spawnPoint, transform.localScale, weirdQuat, mask))
            {
                playerCharacter.transform.position = spawnPoint;
                return;
            }
        }

        // 2.checks the forward most layers relative to the current layer
        if (curLayer != 0)
        {
            int nextLayer = curLayer;

            while (nextLayer >= 0)
            {
                nextLayer -= 1;

                if (layerActive[nextLayer])
                {
                    foreach (Vector2 xyPostion in respawnPostions)
                    {
                        Vector3 spawnPoint = new Vector3(
                            xyPostion.x, xyPostion.y, layerZAxis[nextLayer]);

                        if (!Physics.CheckBox(spawnPoint, transform.localScale, weirdQuat, mask))
                        {
                            depthScript.curDepth = nextLayer;
                            playerCharacter.transform.position = spawnPoint;
                            return;
                        }
                    }
                }
            }
        }

        // 3. brute force search all layers to find an open position
        for (int i = 0; i < layerActive.Length; i++)
        {
            if (layerActive[i])
            {
                foreach (Vector2 xyPostion in respawnPostions)
                {
                    Vector3 spawnPoint = new Vector3(
                        xyPostion.x, xyPostion.y, layerZAxis[i]);

                    if (!Physics.CheckBox(spawnPoint, transform.localScale, weirdQuat, mask))
                    {
                        depthScript.curDepth = i;
                        playerCharacter.transform.position = spawnPoint;
                        return;
                    }
                }
            }
        }

        // 4. default respawn position
        playerCharacter.transform.position = new Vector3(
                defaultRespawn.x, defaultRespawn.y, layerZAxis[curLayer]);
    }

    public void RespawnPlayer()
    {
		fader.ResetFade();

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
                respawnPostions[0].x, respawnPostions[0].y, layerZAxis[index]);
        }
        else
        {
            // alternate layer/position
        }
    }
}

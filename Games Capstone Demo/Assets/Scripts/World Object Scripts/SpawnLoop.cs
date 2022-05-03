using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoop : MonoBehaviour
{
    public float loopSpawnPos = 160;
    public float loopDespawnPos = -30;

    // When the attacted object reaches a certain position
    // on the X axis it changes its position
    void Update()
    {
        Vector3 p = transform.position;
        if (p.x < (loopDespawnPos))
        {
            p.x = loopSpawnPos;
            transform.position = p;  // you can set the position as a whole, just not individual fields
        }
    }
}

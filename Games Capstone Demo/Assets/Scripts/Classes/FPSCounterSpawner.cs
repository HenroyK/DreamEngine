using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject fpsCounter;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerStats.hasSpawnedFPSCounter)
        {
            PlayerStats.hasSpawnedFPSCounter = true;
            Instantiate(fpsCounter);
        }

    }
}

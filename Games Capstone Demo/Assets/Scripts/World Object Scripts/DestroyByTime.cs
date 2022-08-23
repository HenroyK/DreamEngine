using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    // Length of time an object
    // is present for
    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}

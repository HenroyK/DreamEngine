using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public Rigidbody blockRigidbody;
    public float speed;

    // Moves the attached object left at a set speed
    void Update()
    {
        blockRigidbody.velocity = Vector3.left * speed;
    }
}

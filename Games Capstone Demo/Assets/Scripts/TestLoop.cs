using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoop : MonoBehaviour
{
    public float loopLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 p = transform.position;
        if (p.x < (loopLength * -1))
        {
            p.x = loopLength;
            transform.position = p;  // you can set the position as a whole, just not individual fields
        }
    }
}

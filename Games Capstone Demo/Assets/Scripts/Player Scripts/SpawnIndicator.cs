using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicator : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    private GameObject target;
    [SerializeField]
    private float speedChangePercent = 0.3f;
    private float speedChangeDistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawned the effect");
        
        target = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(target.transform.position);
        speedChangeDistance = Vector3.Distance(transform.position, target.transform.position) * (speedChangePercent);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        if (Vector3.Distance(transform.position, target.transform.position) < speedChangeDistance)
        {
            if ((Vector3.Distance(transform.position, target.transform.position) / speedChangeDistance) < 0.5)
            {
                step = step * 0.5f;
            }
            else
            {
                step = step * (Vector3.Distance(transform.position, target.transform.position) / speedChangeDistance);
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        //if (Vector3.Distance(transform.position, target.transform.position) < 0.001f)
        //{
        //    Destroy(this.gameObject);
        //}
    }
}

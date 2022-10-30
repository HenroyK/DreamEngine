using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passable : MonoBehaviour
{
    //Vars
    public bool droppable = false;

    private GameObject player;
    private new Collider collider;
    private bool drop;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        collider = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Non collide if player below collider bounds
        if (collider != null && player != null)
        {
            if (player.transform.position.y <= (transform.position.y + collider.bounds.size.y + 1.5f) || drop)
                collider.enabled = false;
            else
                collider.enabled = true;
        }
        else
        {
            player = GameObject.FindWithTag("Player");
            collider = gameObject.GetComponent<Collider>();
        }
        drop = false;
    }

    public void DropThrough()
    {
        if(droppable)
            drop = true;
    }
}

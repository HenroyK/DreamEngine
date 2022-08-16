using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScoreEffect : MonoBehaviour
{
    private bool pickedUp = true;
    [SerializeField]
    private float speed = 10;
    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PickupEffectTarget");
        Debug.Log(target.transform.position);
    }
    private void Update()
    {
        if (pickedUp)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
        if (Vector3.Distance(transform.position, target.transform.position) < 0.001f)
        {
            Destroy(this.gameObject);
        }

    }
    //public void PickUp()
    //{
    //    pickedUp = true;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Rigidbody rb;
    public float forceMin;
    public float forceMax;

    void Start()
    {
        float force = Random.Range(forceMin, forceMax);
        rb.AddForce(transform.right * force);
        //adds random rotation
        rb.AddTorque(Random.insideUnitSphere * force);
        Destroy(gameObject, 4);
    }
}

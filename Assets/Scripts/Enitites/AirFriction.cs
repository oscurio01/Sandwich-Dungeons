using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFriction : MonoBehaviour
{
    public float airResistance = 0f;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude > 0)
        {
            rb.velocity -= rb.velocity.normalized * airResistance * Time.deltaTime;
        }
    }
}

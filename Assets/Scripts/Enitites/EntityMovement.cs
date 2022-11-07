using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float airResistance = 0f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Movements(Vector2 insert, float speed)
    {
        rb.MovePosition(transform.position + new Vector3(insert.x, 0, insert.y).normalized * speed * Time.fixedDeltaTime);

        //rb.velocity += new Vector3(insert.x, 0, insert.y).normalized * speed;

    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity -= rb.velocity.normalized * airResistance * Time.deltaTime;
        }
    }
}

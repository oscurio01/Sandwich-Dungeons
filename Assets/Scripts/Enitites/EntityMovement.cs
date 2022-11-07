using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [Range(0,1)]public float friction = 0f;
    public float acceleration;
    Rigidbody rb;
    Vector3 accelerate;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Movements(Vector2 insert, float speed)
    {
        //rb.MovePosition(transform.position + new Vector3(insert.x, 0, insert.y).normalized * speed * Time.fixedDeltaTime);
        if (rb.velocity.magnitude == 0 && insert.magnitude <= 0) return;

        accelerate = CalculateVelocity(rb.velocity, new Vector3(insert.x, 0, insert.y).normalized, acceleration, speed, friction);

        rb.velocity = accelerate;

    }

    public static Vector3 CalculateVelocity(Vector3 currentVelocity, Vector3 direction, float acceleration, float limitVelocity = Mathf.Infinity, float friction = 1f)
    {
        Vector3 limVel = direction * limitVelocity;

        return new Vector3(
            Mathf.Lerp(currentVelocity.x, limVel.x, Mathf.Min((acceleration * friction * Time.deltaTime) / Mathf.Abs(currentVelocity.x - limVel.x), 1)),
            Mathf.Lerp(currentVelocity.y, limVel.y, Mathf.Min((acceleration * friction * Time.deltaTime) / Mathf.Abs(currentVelocity.y - limVel.y), 1)),
            Mathf.Lerp(currentVelocity.z, limVel.z, Mathf.Min((acceleration * friction * Time.deltaTime) / Mathf.Abs(currentVelocity.z - limVel.z), 1))
            );
    }

}

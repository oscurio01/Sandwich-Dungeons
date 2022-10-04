using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : CollisionBehaviour
{
    public float pushForce = 20000f;

    Vector3 mine;
    Vector3 notMine;

    public override void ExecuteCollision(GameObject other)
    {
        if(other != gameObject)
        {
            notMine = other.transform.position;
            mine = gameObject.transform.position;

            other.GetComponent<Rigidbody>().AddForce((notMine - mine).normalized * pushForce);
        }
    }
}

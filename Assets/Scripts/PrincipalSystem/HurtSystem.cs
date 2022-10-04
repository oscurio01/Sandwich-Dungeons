using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtSystem : CollisionBehaviour
{
    public int damage = 0;
    public override void ExecuteCollision(GameObject other)
    {
        other.GetComponent<HealthSystem>()?.MakeDamage(damage);
    }
}

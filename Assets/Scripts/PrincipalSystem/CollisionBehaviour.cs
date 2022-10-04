using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionBehaviour : MonoBehaviour
{
    public abstract void ExecuteCollision(GameObject other);
}

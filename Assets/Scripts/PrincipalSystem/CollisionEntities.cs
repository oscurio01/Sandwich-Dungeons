using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class CollisionEntities : MonoBehaviour
{
    public enum Team {White, BLACK }

    public Team team;

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEntities entities = gameObject.GetComponent<CollisionEntities>() != collision.gameObject.GetComponent<CollisionEntities>() ?
            collision.gameObject.GetComponent<CollisionEntities>() : null;

        if (entities && entities.team == team)
        {
            gameObject.GetComponents<CollisionBehaviour>().ToList().ForEach(n => n.ExecuteCollision(collision.gameObject));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CollisionEntities entities = gameObject.GetComponent<CollisionEntities>() != other.gameObject.GetComponent<CollisionEntities>() ?
            other.gameObject.GetComponent<CollisionEntities>() : null;

        if (entities && entities.team == team)
        {
            gameObject.GetComponents<CollisionBehaviour>().ToList().ForEach(n => n.ExecuteCollision(other.gameObject));
        }
    }
}

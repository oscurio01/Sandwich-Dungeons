using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorEntities))]
public class CollisionEntities : MonoBehaviour
{
    public ColorEntities.Team team { get; private set; }

    private void Start()
    {


        team = GetComponent<ColorEntities>().team;
    }

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

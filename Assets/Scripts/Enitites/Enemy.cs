using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class Enemy : SpawnEntities
{
    HealthSystem _health;
    bool onlyOnce = false;
    GameObject currentRoom;
    Rigidbody rb;
    EntityMovement entMovement;

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        onlyOnce = false;
    }

    private void Awake()
    {
        _health = GetComponent<HealthSystem>();
        rb = GetComponent<Rigidbody>();
        entMovement = GetComponent<EntityMovement>();
        alive = true;
        onlyOnce = false;
    }

    private void Update()
    {
        if (_health.isDead && !onlyOnce)
        {
            alive = false;
            currentRoom = CurrentRoom.Instance.GetCurrentRoom();
            currentRoom.GetComponent<RoomSystem>().numEnemies--;
            onlyOnce = true;
        }
        else
        {
            ActionEnemy();
        }
    }

    private void ActionEnemy()
    {
        
    }
}

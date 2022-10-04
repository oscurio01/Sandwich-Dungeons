using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpawnEntities
{
    HealthSystem _health;
    bool onlyOnce;
    GameObject currentRoom;
    Rigidbody rb;

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    private void Awake()
    {
        _health = GetComponent<HealthSystem>();
        rb = GetComponent<Rigidbody>();
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

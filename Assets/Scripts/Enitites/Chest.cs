using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : SpawnEntities
{
    HealthSystem _health;
    bool onlyOnce;
    GameObject currentRoom;

    private void Awake()
    {
        _health = GetComponent<HealthSystem>();
        alive = true;
        onlyOnce = false;
    }

    private void Update()
    {
        if (_health.isDead && !onlyOnce)
        {
            alive = false;
            currentRoom = CurrentRoom.Instance.GetCurrentRoom();

            onlyOnce = true;
        }
        else
        {
            ActionChest();
        }
    }

    private void ActionChest()
    {
        
    }
}

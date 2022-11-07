using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    public string tagEntities;
    GameObject _gameObject;
    protected bool alive = true;
    SpawnEntities child;

    private void Awake()
    {

    }
    public static void Respawn(SpawnEntities ent)
    {
        if ((ent.child != null && !ent.child.alive) || ent.tagEntities == "") return;
         
        ent._gameObject = ent._gameObject == null ? PoolingManager.Instance.GetPooledObjectByTag(ent.tagEntities) : ent._gameObject;
        if (ent._gameObject == null) return;

        if (ent._gameObject.GetComponent<SpawnEntities>()) ent.child = ent._gameObject.GetComponent<SpawnEntities>();


        if (!ent.child.alive) DesSpawn(ent);

        //Debug.Log(ent.gameObject.name +" " + ent.alive + " " + ent.child.name + " " + ent.child.alive);

        ent._gameObject.transform.position = ent.transform.position;
        ent._gameObject.transform.rotation = ent.transform.rotation;

        ent._gameObject.SetActive(true);

        //ent._gameObject.transform.SetParent(ent.gameObject.transform);
    }

    public static void DesSpawn(SpawnEntities ent)
    {
        if (ent._gameObject == null) return;
        else
        {
            //ent._gameObject.transform.SetParent(PoolingManager.Instance.gameObject.transform);
            ent._gameObject.SetActive(false);
        }
    }
}

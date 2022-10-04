using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class RoomSystem : MonoBehaviour
{
    public int Width;
    public int Height;
    public int X;
    public int Y;

    public RoomSystem (int x, int y)
    {
        X = x;
        Y = y;
    }

    public int numEnemies;

    public Door[] doors;

    public bool Open { get { return numEnemies <= 0; }}

    public delegate void TestDelegate(SpawnEntities spawnEntities);

    private bool updateDoors = false;

    private void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You Press play Wrong");
            return;
        }

        doors = gameObject.GetComponentsInChildren<Door>();

        RoomController.instance.RegisterRoom(this);
    }

    private void Update()
    {
        if (Open == false)
        {
            foreach (Door door in doors)
            {
                door.locked = true;
            }
        }
        else
        {
            foreach (Door door in doors)
            {
                door.locked = false;
            }
        }

        if(name.Contains("End") && !updateDoors)
        {
            RemoveUnConnectedDoors();
            updateDoors = true;
        }

        //Debug.Log(Open);
    }

    public void RemoveUnConnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doortype)
            {
                case Door.DoorType.top:
                    if (GetTop() == null) door.gameObject.SetActive(false);
                    else
                        door.gameObject.SetActive(true);
                    break;
                case Door.DoorType.right:
                    if (GetRight() == null) door.gameObject.SetActive(false);
                    else
                        door.gameObject.SetActive(true);
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null) door.gameObject.SetActive(false);
                    else
                        door.gameObject.SetActive(true);
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null) door.gameObject.SetActive(false);
                    else
                        door.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }


        }
    }

    public RoomSystem GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1)) 
            return RoomController.instance.FindRoom(X, Y + 1);
        else
            return null;
    }
    public RoomSystem GetRight()
    {
        if (RoomController.instance.DoesRoomExist(X + 1, Y)) 
            return RoomController.instance.FindRoom(X + 1, Y);
        else
            return null;
    }
    public RoomSystem GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y)) 
            return RoomController.instance.FindRoom(X - 1, Y);
        else
            return null;
    }
    public RoomSystem GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1)) 
            return RoomController.instance.FindRoom(X, Y - 1);
        else
            return null;
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * Width, 0, Y * Height);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y+4.5f, transform.position.z), new Vector3(Width, 10, Height));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            ActivateMethods(other, SpawnEntities.Respawn);

            numEnemies = FindObjectsOfType<Enemy>(true).Where(sr => sr.gameObject.activeInHierarchy).Count();
            CurrentRoom.Instance.NewRoom(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ActivateMethods(other, SpawnEntities.DesSpawn);

    }

    void ActivateMethods(Collider other, TestDelegate method)
    {
        if (other.GetComponent<Player>())
        {
            for (int i = 0; i < GetComponentsInChildren<SpawnEntities>().Length; i++)
            {
                method(GetComponentsInChildren<SpawnEntities>()[i]);
            }


        }
    }
}

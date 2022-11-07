using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RoomTypes
{
    Normal,
    Special
}

public class RoomSystem : MonoBehaviour
{
    public int Width;
    public int Height;
    //public int X;
    //public int Y;
    public Vector2Int _vector2;

    public RoomSystem (Vector2Int positionVector2)
    {
        _vector2 = positionVector2;
    }

    public RoomTypes roomType;

    public int numEnemies;

    public Door[] doors;

    public bool Open { get { return numEnemies <= 0; }}

    public delegate void TestDelegate(SpawnEntities spawnEntities);

    private bool updateDoors = false;

    [SerializeField] private bool _specialRoom = false;

    private void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You Press play Wrong");
            return;
        }

        doors = gameObject.GetComponentsInChildren<Door>();

        _specialRoom = roomType == RoomTypes.Special ? true : false;

        //RoomController.instance.RegisterRoom(this);
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

        //Debug.Log(Open);
    }

    public void RemoveUnConnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doortype)
            {
                case Door.DoorType.top:
                    
                    RoomSystem topRoom = GetTop();

                    if (topRoom && _specialRoom)
                    {
                       
                    }

                    door.gameObject.SetActive(topRoom);

                    break;
                case Door.DoorType.right:

                    RoomSystem rightRoom = GetRight();

                    if (rightRoom)
                    {
                        
                    }

                    door.gameObject.SetActive(rightRoom);

                    break;
                case Door.DoorType.left:

                    RoomSystem leftRoom = GetLeft();

                    if (leftRoom)
                    {
                        
                    }

                    door.gameObject.SetActive(leftRoom);

                    break;
                case Door.DoorType.bottom:

                    RoomSystem bottomRoom = GetBottom();

                    if (bottomRoom)
                    {
                        
                    }

                    door.gameObject.SetActive(bottomRoom);

                    break;
                default:
                    break;
            }


        }
    }

    public RoomSystem GetTop()
    {
        if (RoomController.instance.DoesRoomExist(new Vector2Int(_vector2.x, _vector2.y + 1))) 
            return RoomController.instance.FindRoom(new Vector2Int(_vector2.x, _vector2.y + 1));
        else
            return null;
    }
    public RoomSystem GetRight()
    {
        if (RoomController.instance.DoesRoomExist(new Vector2Int(_vector2.x + 1, _vector2.y))) 
            return RoomController.instance.FindRoom(new Vector2Int(_vector2.x + 1, _vector2.y));
        else
            return null;
    }
    public RoomSystem GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(new Vector2Int(_vector2.x - 1, _vector2.y))) 
            return RoomController.instance.FindRoom(new Vector2Int(_vector2.x - 1, _vector2.y));
        else
            return null;
    }
    public RoomSystem GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(new Vector2Int(_vector2.x, _vector2.y - 1))) 
            return RoomController.instance.FindRoom(new Vector2Int(_vector2.x, _vector2.y - 1));
        else
            return null;
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(_vector2.x * Width, 0, _vector2.y * Height);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        top, right, left, bottom
    }

    public DoorType doortype;

    public Sprite Locked, Unlocked;
    public bool locked;

    CollisionEntities my_collision;
    private float widthOffset = 8f;

    private GameObject currentRoom;

    private void Start()
    {
        my_collision = GetComponent<CollisionEntities>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {

            currentRoom = CurrentRoom.Instance.GetCurrentRoom();

            if(other.GetComponent<CollisionEntities>().team != my_collision.team &&
            currentRoom.GetComponent<RoomSystem>().numEnemies <= 0)
            {
                switch (doortype)
                {
                    case DoorType.top:
                        other.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z + widthOffset);
                        break;
                    case DoorType.right:
                        other.transform.position = new Vector3(transform.position.x + widthOffset, other.transform.position.y, transform.position.z);
                        break;
                    case DoorType.left:
                        other.transform.position = new Vector3(transform.position.x - widthOffset, other.transform.position.y, transform.position.z);
                        break;
                    case DoorType.bottom:
                        other.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z - widthOffset);
                        break;
                    default:
                        break;
                }
            }
        }    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraRoom : MonoBehaviour
{
    public float moveSpeedChangeRoom;
    GameObject currentRoom;
    GameObject lastRoom;
    void Start()
    {
        currentRoom = CurrentRoom.Instance.GetCurrentRoom();
        lastRoom = currentRoom;
    }


    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        currentRoom = CurrentRoom.Instance.GetCurrentRoom();
        if (currentRoom != null)
        {
            Vector3 targetPos = GetCameraTargetPosition();
            if(currentRoom != lastRoom)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), Time.deltaTime * moveSpeedChangeRoom);
                //transform.position = new Vector3(currentRoom.transform.position.x, transform.position.y, currentRoom.transform.position.z);
                if(transform.position == new Vector3(targetPos.x, transform.position.y, targetPos.z))
                    lastRoom = currentRoom;
            }
        }
    }

    Vector3 GetCameraTargetPosition()
    {
        if (currentRoom == lastRoom)
        {
            return Vector3.zero;
        }
        
        Vector3 targetPos = currentRoom.GetComponent<RoomSystem>().GetRoomCenter();
        return targetPos;
    }


}

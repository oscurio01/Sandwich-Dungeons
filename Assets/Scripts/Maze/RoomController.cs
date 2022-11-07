using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public Vector2Int XY;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    public List<RoomSystem> RandomCreatesRooms = new List<RoomSystem>();

    public static Dictionary<Vector2Int, RoomSystem> roomDictionary = new Dictionary<Vector2Int, RoomSystem>();

    //bool isLoadingRoom = false;
    //bool spawnBossRoom = false;
    //bool updatedRooms = false;

    string currentWorldName = "Mazmorra_1_";

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //UpdateRoomQueue();
    }

    public RoomSystem LoadRoom(RoomSystem roomPrefab, Vector2Int XY)
    {
        //RoomInfo newRoomData = new RoomInfo();
        //newRoomData.name = name;
        //newRoomData.XY = XY;

        //LoadRoomQueue.Enqueue(newRoomData);
        RoomSystem room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity).GetComponent<RoomSystem>();
        room.transform.position = new Vector3(
                XY.x * room.Width, 0, XY.y * room.Height);
        room._vector2 = XY;
        room.name = currentWorldName + " " + roomPrefab.name + " " + room._vector2.x + ", " + room._vector2.y;
        room.transform.parent = transform;

        //roomDictionary.Add(XY, room);
        return room;
    }


    public bool DoesRoomExist(Vector2Int XY)
    {
        roomDictionary.TryGetValue(XY, out RoomSystem rD);
        return rD;
    }

    public RoomSystem FindRoom(Vector2Int XY)
    {
        //return loadedRooms.Find(item => item._vector2 == XY);
        roomDictionary.TryGetValue(XY, out RoomSystem rD);
        return rD;
    }
}

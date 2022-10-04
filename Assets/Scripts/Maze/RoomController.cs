using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    public List<RoomSystem> RandomCreatesRooms = new List<RoomSystem>();

    public List<RoomSystem> loadedRooms = new List<RoomSystem>();

    bool isLoadingRoom = false;
    bool spawnBossRoom = false;
    bool updatedRooms = false;

    string currentWorldName = "Mazmorra_1_";

    RoomInfo currentLoadRoomData;

    Queue<RoomInfo> LoadRoomQueue = new Queue<RoomInfo>();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

    }

    private void Update()
    {
        UpdateRoomQueue();
    }
    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if(LoadRoomQueue.Count == 0)
        {
            if (!spawnBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if(spawnBossRoom && !updatedRooms)
            {
                foreach (RoomSystem room in loadedRooms)
                {
                    room.RemoveUnConnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = LoadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    public void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x, y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        LoadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;
        for (int i = 0; i < RandomCreatesRooms.Count; i++)
        {
            if(info.name == RandomCreatesRooms[i].name)
            {
                Instantiate(RandomCreatesRooms[i], Vector3.zero, Quaternion.identity);
                yield return null;
            }
        }
    }

    public void RegisterRoom(RoomSystem room)
    {
        if (DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
        else
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width,
                0,
                currentLoadRoomData.Y * room.Height

            );
            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            loadedRooms.Add(room);

        }

    }

    IEnumerator SpawnBossRoom()
    {
        spawnBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if(LoadRoomQueue.Count == 0)
        {
            RoomSystem bossRoom = loadedRooms[loadedRooms.Count - 1];
            RoomSystem tmpRoom = new RoomSystem(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);

            var roomToRemove = loadedRooms.Single(r => r.X == tmpRoom.X && r.Y == tmpRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tmpRoom.X, tmpRoom.Y);
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public RoomSystem FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }
}

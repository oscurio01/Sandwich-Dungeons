using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonData;

    private Queue<Vector2Int> branch = new Queue<Vector2Int>();

    [SerializeField] private BiomeData biomeData;

    private void Start()
    {
        SpawnRooms();
    }

    private void SpawnRooms()
    {
        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax+1);
        int range;
        int range2;
        bool abort;
        Vector2Int actualBranch;
        Vector2Int rS2;

        //RoomController.instance.LoadRoom("Start", Vector2Int.zero);

        branch.Enqueue(Vector2Int.zero);

        RoomController.roomDictionary.Add(Vector2Int.zero, null);

        int nRoom = 1;

        while (nRoom < iterations)
        {

            actualBranch = branch.Dequeue();

            if(branch.Count == 0)
            {
                range = Random.Range(1, 4);
            }
            else if(branch.Count >= 3)
            {
                range = Random.Range(0, 2);
            }
            else if(branch.Count >= 2)
            {
                range = Random.Range(0, 3);
            }
            else
            {
                range = Random.Range(0, 4);
            }

            //Debug.Log("A " + actualBranch + " " +range);

            for (int x = 0; x < range; x++)
            {
                abort = true;
                range2 = Random.Range(0, 4);

                for (int z = 0; z < 4; z++)
                {
                    if (!RoomController.roomDictionary.ContainsKey(actualBranch + GetDirection(range2)) &&
                        GetNearbyRoomNumber(actualBranch + GetDirection(range2)) < 2)
                    {

                        abort = false;
                        break;
                    }

                    range2++;
                    range2 = range2 % 4;
                    
                }

                if (!abort)
                {
                    rS2 = actualBranch + GetDirection(range2);
                    //RoomController.instance.LoadRoom("Empty", actualBranch + GetDirection(range2));

                    branch.Enqueue(rS2);
                    RoomController.roomDictionary.Add(rS2, null);

                    nRoom++;
                    if(nRoom >= iterations)break;       
                }


            }

        }

        Vector2Int[] arraykeysRoom = RoomController.roomDictionary.Keys.ToArray();

        for (int i = 0; i < arraykeysRoom.Length; i++)
        {
            Vector2Int key = arraykeysRoom[i];

            if (key == Vector2Int.zero) // Start
            {
                RoomController.roomDictionary[key] = RoomController.instance.LoadRoom(biomeData.StartRoom, key);
            }
            else if (GetNearbyRoomNumber(key) == 1 && key != Vector2Int.zero) // Special
            {
                if(key == arraykeysRoom[arraykeysRoom.Length-1])
                {
                    RoomController.roomDictionary[key] = RoomController.instance.LoadRoom(GetRandomRoom(biomeData.prefabBossRoom), key);
                }
                else
                    RoomController.roomDictionary[key] = RoomController.instance.LoadRoom(GetRandomRoom(biomeData.prefabSpecialRoom[0]), key);

                //Debug.Log("B " + key + " " + i);
            }
            else // Empty room or enemy room
            {
                RoomController.roomDictionary[key] = RoomController.instance.LoadRoom(GetRandomRoom(biomeData.prefabNormaRoom), key);
            }
        }

        
    }

    Vector2Int GetDirection(int value)
    {
        switch (value)
        {
            case 0:
                return Vector2Int.up;
            case 1:
                return Vector2Int.right;
            case 2:
                return Vector2Int.down;
            case 3:
                return Vector2Int.left;
            default:
                return Vector2Int.zero;
        }
    }

    int GetNearbyRoomNumber(Vector2Int center)
    {
        int value = 0;
        for (int i = 0; i < 4; i++)
        {
            if(RoomController.roomDictionary.ContainsKey(center + GetDirection(i)))
            {
                value++;
            }
        }

        return value;
    }

    public RoomSystem GetRandomRoom(BiomeData.SameRoomVariants variant)
    {
        return variant.variants[Random.Range(0, variant.variants.Count)];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour
{
    private static CurrentRoom _instance;
    public static CurrentRoom Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CurrentRoom>();

            }
            return _instance;

        }
    }

    public GameObject currentObject;

    public GameObject GetCurrentRoom()
    {
        return currentObject;
    }

    public void NewRoom(GameObject newRoom)
    {
        currentObject = newRoom;
    }
}

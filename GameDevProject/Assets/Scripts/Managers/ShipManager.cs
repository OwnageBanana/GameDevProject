using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour {




    // because unity doesnt support in editor modification of dictionaries and This datatype is really useful
    public GameObject[] roomTypes;
    private Dictionary<string, GameObject> roomTypesDict;

    //MaxShipSize/2 is the (0,0) coordinate in the world
    public int MaxShipSize = 11;
    public int center;

    private float roomSize = 3.96f;

    private GameObject[,] rooms;


    public ShipManager()
    {
    }

    public void initalizeRooms()
    {
        roomTypesDict = new Dictionary<string, GameObject>();
        //populating the dictionary with the different rooms
        foreach (GameObject room in roomTypes)
            roomTypesDict.Add(room.tag, room);

        //initalizing some variables for ship building
        rooms = new GameObject[MaxShipSize, MaxShipSize];
        center = (int)Mathf.Ceil(((float)MaxShipSize) / 2);
    }

    public void InitalizeShip()
    {
        //initalizing ship
        rooms[center, center] = Instantiate(roomTypesDict["PilotBay"], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        rooms[center, center - 1] = Instantiate(roomTypesDict["Engine"], new Vector3(0, 0, -roomSize), new Quaternion(0, 0, 0, 0));
        rooms[center, center - 2] = Instantiate(roomTypesDict["AICore"], new Vector3(0, 0, -roomSize * 2), new Quaternion(0, 0, 0, 0));
        rooms[center + 1, center - 1] = Instantiate(roomTypesDict["Gym"], new Vector3(roomSize, 0, -roomSize), new Quaternion(0, 0, 0, 0));
        rooms[center + 1, center - 2] = Instantiate(roomTypesDict["Reactor"], new Vector3(roomSize, 0, -roomSize * 2), new Quaternion(0, 0, 0, 0));
        rooms[center - 1, center - 1] = Instantiate(roomTypesDict["Cafeteria"], new Vector3(-roomSize, 0, -roomSize), new Quaternion(0, 0, 0, 0));
        rooms[center - 1, center - 2] = Instantiate(roomTypesDict["Kitchen"], new Vector3(-roomSize, 0, -roomSize * 2), new Quaternion(0, 0, 0, 0));
    }


    public RoomAttribute AddRoom()
    {

        RoomAttribute t = new RoomAttribute();


        return t;

    }
}

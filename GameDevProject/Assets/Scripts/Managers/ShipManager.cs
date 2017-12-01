using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipManager : MonoBehaviour
{




    // because unity doesnt support in editor modification of dictionaries and This datatype is really useful
    public GameObject[] roomTypes;
    private Dictionary<string, GameObject> roomTypesDict;

    public List<NavMeshSurface> surfaces;

    //MaxShipSize/2 is the (0,0) coordinate in the world
    public int MaxShipSize = 11;
    public int center;

    private float roomSize = 3.96f;

    private GameObject[,] rooms;

    public void initalizeRooms()
    {
        roomTypesDict = new Dictionary<string, GameObject>();
        //populating the dictionary with the different rooms
        foreach (GameObject room in roomTypes)
        {
            roomTypesDict.Add(room.tag, room);
        }
        //initalizing some variables for ship building
        rooms = new GameObject[MaxShipSize, MaxShipSize];
        center = (int)Mathf.Ceil(((float)MaxShipSize) / 2);

    }

    public void InitalizeShip()
    {
        //initalizing ship
        InitializeRoomAtPos(0, 0,"PilotBay");
        InitializeRoomAtPos(0, -1, "Engine");
        InitializeRoomAtPos(0, -2, "AICore");
        InitializeRoomAtPos(1, -1, "Gym");
        InitializeRoomAtPos(1, -2,"Reactor");
        InitializeRoomAtPos(-1, -1, "Cafeteria");
        InitializeRoomAtPos(-1, -2, "Kitchen");

        if (surfaces[0] != null)
            surfaces[0].BuildNavMesh();

    }
    private void InitializeRoomAtPos(int xFromCenter, int zFromCenter,string tag)
    {
        rooms[center + xFromCenter, center + zFromCenter] = Instantiate(roomTypesDict[tag], new Vector3(roomSize * xFromCenter, 0, roomSize* zFromCenter), new Quaternion(0, 0, 0, 0));
        surfaces.Add(rooms[center + xFromCenter, center + zFromCenter].GetComponent<NavMeshSurface>());
        var atb = rooms[center + xFromCenter, center + zFromCenter].GetComponent<RoomAttribute>();
        atb.x = center + xFromCenter;
        atb.z = center + zFromCenter;
    }

    public RoomAttribute AddRoom()
    {
        RoomAttribute t = new RoomAttribute();


        return t;

    }
}

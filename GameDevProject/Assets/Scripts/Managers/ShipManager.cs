using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipManager : MonoBehaviour
{




    // because unity doesnt support in editor modification of dictionaries and This datatype is really useful
    public GameObject[] roomTypes;
    public RoomManager roomManager;

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
        InitializeRoomAtPos(center-1, 0, "PilotBay");
        InitializeRoomAtPos(center-1, 1, "Engine");
        InitializeRoomAtPos(center-1, 2, "AICore");
        InitializeRoomAtPos(center, 1, "Gym");
        InitializeRoomAtPos(center, 2, "Reactor");
        InitializeRoomAtPos(center - 2, 1, "Cafeteria");
        InitializeRoomAtPos(center - 2, 2, "Kitchen");



    }
    private void InitializeRoomAtPos(int x, int z, string tag)
    {
        //initalizing the room in hte world space
        rooms[x, z] = Instantiate(roomTypesDict[tag], new Vector3((roomSize * x) - (center * roomSize) , 0,(center * roomSize) + ( roomSize * -z)), new Quaternion(0, 0, 0, 0));
        surfaces.Add(rooms[x, z].GetComponent<NavMeshSurface>());
        var atb = rooms[x, z].GetComponent<RoomAttribute>();
        atb.x = x;
        atb.z = z;
        atb.Room = tag;

        if (surfaces[0] != null)
        {
            surfaces[0].BuildNavMesh();
            var people = GetComponents<RandomWalk>();
            foreach (RandomWalk person in people)
                person.ResetNavMesh();
        }
    }

    public void AddRoom(string tag)
    {
        Dictionary<int, List<int>> freeSpaces = new Dictionary<int, List<int>>();
        //initing the dictionary so no null pointer exceptions
        for (int i = 0; i < MaxShipSize; i++)
            freeSpaces.Add(i, new List<int>());

        //lazy alg for finding empty spaces in the room
        for (int i = 0; i < MaxShipSize; i++) {
            for (int j = 0; j < MaxShipSize; j++)
            {
                // if room exists at j,i, test spaces around it to add as empty spaces
                if (rooms[j, i] != null)
                {
                    if (j - 1 >= 0)
                    {
                        if (rooms[j - 1, i] == null)
                            if (!freeSpaces[j - 1].Contains(i))
                                freeSpaces[j - 1].Add(i);

                    }
                    if (j + 1 < MaxShipSize)
                    {
                        if (rooms[j + 1, i] == null)
                            if (!freeSpaces[j + 1].Contains(i))
                                freeSpaces[j + 1].Add(i);
                    }
                    if (i - 1 >= 0)
                    {
                        if (rooms[j, i - 1] == null)
                            if (!freeSpaces[j].Contains(i - 1))
                                freeSpaces[j].Add(i- 1);

                    }
                    if (i + 1 < MaxShipSize)
                    {
                        if (rooms[j, i + 1] == null)
                            if (!freeSpaces[j].Contains(i))
                                freeSpaces[j].Add(i+1);
                    }
                }
            }
        }

        List<KeyValuePair<int, int>> toSelect = new List<KeyValuePair<int, int>>();
        for (int i = 0; i < MaxShipSize; i++)
            foreach (int row in freeSpaces[i])
                toSelect.Add(new KeyValuePair<int, int>(i, row));

        if (toSelect.Count != 0)
        {
            int r = Random.Range(0, toSelect.Count - 1);
            InitializeRoomAtPos(toSelect[r].Key, toSelect[r].Value, tag);
        }

    }

    public void EnableRoom()
    {
        roomManager.selectedRoom.roomEnabled = true;
        var selectedRoom = rooms[roomManager.selectedRoom.x, roomManager.selectedRoom.z].GetComponent<RoomAttribute>();
        selectedRoom.roomEnabled = roomManager.selectedRoom.roomEnabled;

        var lights = rooms[roomManager.selectedRoom.x, roomManager.selectedRoom.z].GetComponentsInChildren<Light>();

        roomManager.Enabled.isOn = true;

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = 6.8f;
        }
    }

    public void DisableRoom()
    {
        roomManager.selectedRoom.roomEnabled = false;
        var selectedRoom = rooms[roomManager.selectedRoom.x, roomManager.selectedRoom.z].GetComponent<RoomAttribute>();
        selectedRoom.roomEnabled = roomManager.selectedRoom.roomEnabled;

        roomManager.Enabled.isOn = false;

        var lights = rooms[roomManager.selectedRoom.x, roomManager.selectedRoom.z].GetComponentsInChildren<Light>();

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity =  0f;
        }
    }

}

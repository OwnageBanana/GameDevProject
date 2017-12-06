//Author: Adam Mills

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipManager : MonoBehaviour
{
    // because unity doesnt support in editor modification of dictionaries and This datatype is really useful
    public GameObject[] roomTypes;
    public RoomManager roomManager;
    public List<GameObject> People;


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
        InitializeRoomAtPos(center - 1, 0, "PilotBay");
        InitializeRoomAtPos(center - 1, 1, "Engine");
        InitializeRoomAtPos(center - 1, 2, "AICore");
        InitializeRoomAtPos(center, 1, "Gym");
        InitializeRoomAtPos(center, 2, "Reactor");
        InitializeRoomAtPos(center - 2, 1, "Cafeteria");
        InitializeRoomAtPos(center - 2, 2, "Kitchen");

    }

    public void AddRandomRoom()
    {
        int r = Random.Range(0, roomTypes.Length - 1);
        AddRoom(roomTypes[r].tag);
    }

    private void InitializeRoomAtPos(int x, int z, string tag)
    {
        //initalizing the room in hte world space
        rooms[x, z] = Instantiate(roomTypesDict[tag], new Vector3((roomSize * x) - (center * roomSize), 0, (center * roomSize) + (roomSize * -z)), new Quaternion(0, 0, 0, 0));
        surfaces.Add(rooms[x, z].GetComponent<NavMeshSurface>());
        var atb = rooms[x, z].GetComponent<RoomAttribute>();
        atb.x = x;
        atb.z = z;
        atb.Room = tag;

        SpawnPerson(x, z);
        if (surfaces[0] != null)
        {
            surfaces[0].BuildNavMesh();
        }
    }

    public void SpawnPerson(int x, int z)
    {
        int r = Random.Range(0, People.Count - 1);
        //spawn a random person in new room in doorway ( hence rooroomSize/2)
        Instantiate(People[r], new Vector3((roomSize * x) - (center * roomSize) - (roomSize/2), 0.5f, (center * roomSize) + (roomSize * -z)), new Quaternion(0, 0, 0, 0));
    }

    public void AddRoom(string tag)
    {
        Dictionary<int, List<int>> freeSpaces = new Dictionary<int, List<int>>();
        //initing the dictionary so no null pointer exceptions
        for (int i = 0; i < MaxShipSize; i++)
            freeSpaces.Add(i, new List<int>());

        //lazy alg for finding empty spaces in the room
        for (int i = 0; i < MaxShipSize; i++)
        {
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
                                freeSpaces[j].Add(i - 1);

                    }
                    if (i + 1 < MaxShipSize)
                    {
                        if (rooms[j, i + 1] == null)
                            if (!freeSpaces[j].Contains(i))
                                freeSpaces[j].Add(i + 1);
                    }
                }
            }
        }

        //list of positions in the rooms array that new rooms can be placed at
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

    /// <summary>
    ///sums all costs and gains of all active rooms
    /// </summary>
    /// <returns>returns the cost/gain for all rooms as a resource collection</returns>
    public Resources GetResourceDeltas()
    {
        Resources delta = new Resources();


        for (int i = 0; i < MaxShipSize; i++)
        {
            for (int j = 0; j < MaxShipSize; j++)
            {
                var roomAtb = rooms[j, i].GetComponent<RoomAttribute>();
                if (roomAtb.roomEnabled)
                {
                    delta.Food += roomAtb.gain.Food;
                    delta.Energy += roomAtb.gain.Energy;
                    delta.ShipHp += roomAtb.gain.ShipHp;
                    delta.Happiness += roomAtb.gain.Happiness;
                    delta.Garbage += roomAtb.gain.Garbage;
                    delta.Karma += roomAtb.gain.Karma;

                    delta.Food -= roomAtb.cost.Food;
                    delta.Energy -= roomAtb.cost.Energy;
                    delta.ShipHp -= roomAtb.cost.ShipHp;
                    delta.Happiness -= roomAtb.cost.Happiness;
                    delta.Garbage -= roomAtb.cost.Garbage;
                    delta.Karma -= roomAtb.cost.Karma;
                }
            }
        }

        return delta;
    }

    /// <summary>
    /// enables the room based on the ui. Changes lights in the room to indicate to player
    /// </summary>
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

    /// <summary>
    /// disables the room based on the ui. Changes lights in the room to indicate to player
    /// </summary>
    public void DisableRoom()
    {
        roomManager.selectedRoom.roomEnabled = false;
        var selectedRoom = rooms[roomManager.selectedRoom.x, roomManager.selectedRoom.z].GetComponent<RoomAttribute>();
        selectedRoom.roomEnabled = roomManager.selectedRoom.roomEnabled;

        roomManager.Enabled.isOn = false;

        var lights = rooms[roomManager.selectedRoom.x, roomManager.selectedRoom.z].GetComponentsInChildren<Light>();

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity = 0f;
        }
    }

}

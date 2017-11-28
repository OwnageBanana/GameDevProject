using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public DialogueManager dialogueManager;
    public EventManager eventManager;
    public ResourceManager resourceManager;
    public ShipManager shipManager;
    //public AudioManager audioManager;

    public static GameController instance;

    // because unity doesnt support in editor modification of dictionaries and This datatype is really useful
    public GameObject[] roomTypes;
    private Dictionary<string, GameObject> roomTypesDict;


    //MaxShipSize/2 is the (0,0) coordinate in the world
    public int MaxShipSize = 11;
    public int center;
    private bool messageSent = false;
    private Event currentEvent;

    private float roomSize = 3.96f;

    private GameObject[,] rooms;



    //called when game object is created, before awake, this is pretty important.
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            DestroyObject(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    // Use this for initialization
    void Start()
    {
        roomTypesDict = new Dictionary<string, GameObject>();
        //populating the dictionary with the different rooms
        foreach (GameObject room in roomTypes)
            roomTypesDict.Add(room.tag, room);

        //initalizing some variables for ship building
        rooms = new GameObject[MaxShipSize, MaxShipSize];
        center = (int)Mathf.Ceil(((float)MaxShipSize) / 2);

        //initalizing ship
        rooms[center, center] = Instantiate(roomTypesDict["PilotBay"], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        rooms[center, center - 1] = Instantiate(roomTypesDict["Engine"], new Vector3(0, 0, -roomSize), new Quaternion(0, 0, 0, 0));
        rooms[center, center - 2] = Instantiate(roomTypesDict["AICore"], new Vector3(0, 0, -roomSize * 2), new Quaternion(0, 0, 0, 0));
        rooms[center + 1, center - 1] = Instantiate(roomTypesDict["Gym"], new Vector3(roomSize, 0, -roomSize), new Quaternion(0, 0, 0, 0));
        rooms[center + 1, center - 2] = Instantiate(roomTypesDict["Reactor"], new Vector3(roomSize, 0, -roomSize * 2), new Quaternion(0, 0, 0, 0));
        rooms[center - 1, center - 1] = Instantiate(roomTypesDict["Cafeteria"], new Vector3(-roomSize, 0, -roomSize), new Quaternion(0, 0, 0, 0));
        rooms[center - 1, center - 2] = Instantiate(roomTypesDict["Kitchen"], new Vector3(-roomSize, 0, -roomSize * 2), new Quaternion(0, 0, 0, 0));



    }

    // Update is called once per frame
    void Update()
    {

        if (!messageSent)
        {
            Debug.Log("sending message to the dialog manager");

            messageSent = true;
            Dialogue d = new Dialogue { Name = "Test Message", Sentences = new string[] { "this is the first sentence in the test", "this is the second sentence in the test", "Finally, here is the last sentence in the test" } };
            dialogueManager.StartDailogue(d);
            Event e = new Event { Description = "This is a test to see how events work" };
            dialogueManager.StartEvent(e);
        }
    }

    public void AcceptEvent()
    {
        Debug.Log("Event was accepted");

        dialogueManager.EndEvent();
    }

    public void DenyEvent()
    {
        Debug.Log("Event Was Denied");
        dialogueManager.EndEvent();

    }

}

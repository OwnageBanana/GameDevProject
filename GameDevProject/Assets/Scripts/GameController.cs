using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{

    public DialogueManager dialogueManager;
    public EventManager eventManager;
    public ResourceManager resourceManager;
    public ShipManager shipManager;
    public AudioManager audioManager;


    public Camera MainCamera;
    public LayerMask clickablesLayer;

    public static GameController instance;

    public Dialogue startDialogue;

    public int eventIntervalMin;
    public int eventIntervalMax;
    private float timeToEvent;

    public int roomSpawnIntervalMin;
    public int roomSpawnIntervalMax;
    public float timeToRoomSpawn;

    private Event currentEvent;

    private bool messageSent = false;



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
        shipManager.initalizeRooms();
        shipManager.InitalizeShip();
    }

    // Update is called once per frame
    void Update()
    {
        // if left click with mouse and the menu isn't already open
        if (Input.GetMouseButtonDown(0) && !shipManager.roomManager.Panel.activeSelf)
        {
            RaycastHit rayHit;
            bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, clickablesLayer);
            if (hit)
            {
                RoomAttribute room = rayHit.collider.GetComponent<RoomAttribute>();
                if (room != null)
                    shipManager.roomManager.DisplayMenu(room);

            }

        }
        checkSentMessage();

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


    private void checkSentMessage()
    {
        if (!messageSent)
        {
            messageSent = true;
            dialogueManager.StartDailogue(startDialogue);
            Event e = new Event { Description = "This is a test to see how events work" };
            dialogueManager.StartEvent(e);
        }
    }

}

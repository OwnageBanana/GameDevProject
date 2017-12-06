//Author: Adam Mills

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public DialogueManager dialogueManager;
    public EventManager eventManager;
    public ResourceManager resourceManager;
    public ShipManager shipManager;
    public AudioManager audioManager;


    public Camera MainCamera;
    public LayerMask clickablesLayer;

    public Slider progressBar;
    public float GameLength;
    public float progress;

    public static GameController instance;

    public Dialogue startDialogue;

    public int eventIntervalMin;
    public int eventIntervalMax;
    private float timeToEvent;
    private float eventTimer;


    public int roomSpawnIntervalMin;
    public int roomSpawnIntervalMax;
    public float timeToRoomSpawn;
    private float roomTimer;

    public float resourceTick;

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
        setEasy();
    }

    // Update is called once per frame
    void Update()
    {
        //updating progress bar, checking if player won game
        progress += Time.deltaTime;
        progressBar.value = progress / GameLength;


        //Player has won, end the game
        if (progress / GameLength >= 1)
            ;

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

        resourceTick += Time.deltaTime;

        if (resourceTick >= 30)
            resourceManager.AddResources(shipManager.GetResourceDeltas());

        eventTimer += Time.deltaTime;

        if (eventTimer >= timeToEvent)
        {
            currentEvent = eventManager.PickEvent(resourceManager.resources);
            eventTimer = 0;
            dialogueManager.StartEvent(currentEvent);

            timeToEvent = UnityEngine.Random.Range(eventIntervalMin, eventIntervalMax);
        }

        roomTimer += Time.deltaTime;
        if (roomTimer >= timeToRoomSpawn)
        {
            shipManager.AddRandomRoom();
            timeToRoomSpawn = UnityEngine.Random.Range(roomSpawnIntervalMin, roomSpawnIntervalMax);

        }
        checkSentMessage();




    }

    public void AcceptEvent()
    {
        resourceManager.RemoveResources(currentEvent.Cost);
        resourceManager.AddResources(currentEvent.Reward);
        dialogueManager.EndEvent();
        currentEvent = null;
    }

    public void DenyEvent()
    {
        dialogueManager.EndEvent();
        currentEvent = null;
    }


    private void checkSentMessage()
    {
        if (!messageSent)
        {
            messageSent = true;
            dialogueManager.StartDailogue(startDialogue);
        }
    }

    // change event occurance, room spawn game length
    public void setEasy()
    {
        GameLength = 8 * 60;
        eventIntervalMin = 30;
        eventIntervalMax = 40;

        roomSpawnIntervalMin = 30;
        roomSpawnIntervalMax = 60;
    }
    public void setMedium()
    {
        GameLength = 12 * 60;

        eventIntervalMin = 30;
        eventIntervalMax = 40;

        roomSpawnIntervalMin = 30;
        roomSpawnIntervalMax = 40;
    }

    public void setHard()
    {
        GameLength = 15 * 60;
        eventIntervalMin = 30;
        eventIntervalMax = 40;

        roomSpawnIntervalMin = 20;
        roomSpawnIntervalMax = 40;
    }

}

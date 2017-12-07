//Author: Adam Mills

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    //seperate managers that deal with data and helper methods
    public DialogueManager dialogueManager;
    public EventManager eventManager;
    public ResourceManager resourceManager;
    public ShipManager shipManager;
    public AudioManager audioManager;

    //main camera and clickable layers to click on rooms
    public Camera MainCamera;
    public LayerMask clickablesLayer;

    //ui element and values to determine when game is won
    public Slider progressBar;
    public float GameLength;
    public float progress;

    //the start dialog. see text array in the gamecontroller object in unity
    public Dialogue startDialogue;

    //event interval for a random range
    public int eventIntervalMin;
    public int eventIntervalMax;
    //counter for event timer and event time
    private float timeToEvent;
    private float eventTimer;

    //room iterval for a rndom range
    public int roomSpawnIntervalMin;
    public int roomSpawnIntervalMax;
    //room spawn counter and the holder for the time value
    public float timeToRoomSpawn;
    private float roomTimer;

    //counter for resource accumulation
    public float resourceTick;

    //current event object to hold event when event is triggered
    private Event currentEvent;

    //used for the begining dialogue
    private bool messageSent = false;




    // Use this for initialization
    void Start()
    {
        shipManager.initalizeRooms();
        shipManager.InitalizeShip();
        setMedium();
    }

    // Update is called once per frame
    void Update()
    {
        //updating progress bar, checking if player won game
        progress += (Time.deltaTime) * shipManager.GetEngineCount();
        progressBar.value = progress / GameLength;


        //Player has won, end the game
        if (progress / GameLength >= 1)
            SceneManager.LoadScene("Victory");


        if (resourceManager.resources.Happiness > 20)
        {
            audioManager.play("NormalSounds");
            audioManager.stop("AngrySounds");
        }
        else
        {
            audioManager.stop("NormalSounds");
            audioManager.play("AngrySounds");
        }

        // if left click with mouse and the menu isn't already open
        if (Input.GetMouseButtonDown(0) && !shipManager.roomManager.Panel.activeSelf)
        {
            RaycastHit rayHit;
            bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out rayHit, clickablesLayer);
            //if the ray cast hit a room
            if (hit)
            {
                RoomAttribute room = rayHit.collider.GetComponent<RoomAttribute>();
                //if the room isn't null
                if (room != null)
                    //show menu for the room
                    shipManager.roomManager.DisplayMenu(room);

            }

        }

        //resource timer loop
        resourceTick += Time.deltaTime;

        if (resourceTick >= 10)
        {
            if (resourceManager.CheckLost())
                SceneManager.LoadScene("Lost");
            resourceTick = 0;
            resourceManager.ChangeResources(shipManager.GetResourceDeltas());
        }
        //event timer loop
        eventTimer += Time.deltaTime;

        if (eventTimer >= timeToEvent)
        {
            //pick an event to run
            currentEvent = eventManager.PickEvent(resourceManager.resources);
            //display event
            dialogueManager.StartEvent(currentEvent);
            //reset timer
            eventTimer = 0;
            timeToEvent = UnityEngine.Random.Range(eventIntervalMin, eventIntervalMax);
        }

        //room timer loop
        roomTimer += Time.deltaTime;
        if (roomTimer >= timeToRoomSpawn)
        {
            //spawn room
            shipManager.AddRandomRoom();
            //reset room timer
            roomTimer = 0;
            timeToRoomSpawn = UnityEngine.Random.Range(roomSpawnIntervalMin, roomSpawnIntervalMax);

        }
        checkSentMessage();

    }

    /// <summary>
    /// Accepts the current event, adds and subtracts resources and plays noises based on event
    /// </summary>
    public void AcceptEvent()
    {
        //playing noises based on event type
        if (currentEvent.EventType == EventType.Good)
            audioManager.play("GoodEvent");
        else if (currentEvent.EventType == EventType.Bad)
            audioManager.play("BadEvent");

        //special event noise
        if (currentEvent.Description.Contains("HighPitchedNoise"))
            audioManager.play("Noise");

        //resource addition from reward
        resourceManager.AddResources(currentEvent.Reward);
        //resource subtraction from cost
        resourceManager.RemoveResources(currentEvent.Cost);
        dialogueManager.EndEvent();
        currentEvent = null;
    }

    /// <summary>
    /// player denied the event, don't do anything but end the event and set current event to null
    /// </summary>
    public void DenyEvent()
    {
        dialogueManager.EndEvent();
        currentEvent = null;
    }


    /// <summary>
    ///used to display the start message
    /// </summary>
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
        //setting the game intervals
        GameLength = 8 * 60;
        eventIntervalMin = 20;
        eventIntervalMax = 30;
        timeToEvent = UnityEngine.Random.Range(eventIntervalMin, eventIntervalMax);

        roomSpawnIntervalMin = 30;
        roomSpawnIntervalMax = 60;
        timeToRoomSpawn = UnityEngine.Random.Range(roomSpawnIntervalMin, roomSpawnIntervalMax);
    }
    // change event occurance, room spawn game length
    public void setMedium()
    {
        //setting the game intervals
        GameLength = 12 * 60;
        eventIntervalMin = 10;
        eventIntervalMax = 20;
        timeToEvent = UnityEngine.Random.Range(eventIntervalMin, eventIntervalMax);

        roomSpawnIntervalMin = 30;
        roomSpawnIntervalMax = 40;
        timeToRoomSpawn = UnityEngine.Random.Range(roomSpawnIntervalMin, roomSpawnIntervalMax);
    }

    // change event occurance, room spawn game length
    public void setHard()
    {
        //setting the game intervals
        GameLength = 15 * 60;
        eventIntervalMin = 10;
        eventIntervalMax = 10;
        timeToEvent = UnityEngine.Random.Range(eventIntervalMin, eventIntervalMax);

        roomSpawnIntervalMin = 20;
        roomSpawnIntervalMax = 40;
        timeToRoomSpawn = UnityEngine.Random.Range(roomSpawnIntervalMin, roomSpawnIntervalMax);
    }

}

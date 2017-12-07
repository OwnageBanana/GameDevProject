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
    //private void Awake()
    //{
    //
    //    if (instance == null)
    //        instance = this;
    //    else
    //    {
    //        DestroyObject(gameObject);
    //    }
    //    DontDestroyOnLoad(gameObject);
    //
    //}
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
            if (hit)
            {
                RoomAttribute room = rayHit.collider.GetComponent<RoomAttribute>();
                if (room != null)
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

    public void AcceptEvent()
    {
        if (currentEvent.EventType == EventType.Good)
            audioManager.play("GoodEvent");
        else if (currentEvent.EventType == EventType.Bad)
            audioManager.play("BadEvent");
        if (currentEvent.Description.Contains("HighPitchedNoise"))
            audioManager.play("Noise");

        resourceManager.AddResources(currentEvent.Reward);
        resourceManager.RemoveResources(currentEvent.Cost);
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
        GameLength = 15 * 60;
        eventIntervalMin = 10;
        eventIntervalMax = 10;
        timeToEvent = UnityEngine.Random.Range(eventIntervalMin, eventIntervalMax);

        roomSpawnIntervalMin = 20;
        roomSpawnIntervalMax = 40;
        timeToRoomSpawn = UnityEngine.Random.Range(roomSpawnIntervalMin, roomSpawnIntervalMax);
    }

}

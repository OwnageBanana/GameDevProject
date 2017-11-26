using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public DialogueManager dialogueManager;
    public EventManager eventManager;
    public ResourceManager resourceManager;
    //public AudioManager audioManager;

    public static GameController instance;
    bool test = true;
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
    void Start () {

    }

	// Update is called once per frame
	void Update () {

        if (test)
        {
            Debug.Log("sending message to the dialog manager");

            test = false;
            Dialogue d = new Dialogue { Name = "test", Sentences = new string[] { "this is the first sentence in the test", "this is the second sentence in the test", "Finally, here is the last sentence in the test" } };
            dialogueManager.StartDailogue(d);
        }
    }
}

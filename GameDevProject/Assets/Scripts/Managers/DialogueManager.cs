using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;

    public Text DialogueText;
    public Text DialogueTitle;

    public Text MessageText;
    public Text MessageTitle;

    public Animator Dialogue;
    public Animator Message;



    private Coroutine currentDialogue;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    //types out the sentence in thhe dialogue box
    IEnumerator TypeSentence(string sentence)
    {
        //plays the sound of talking
        var audio_m = FindObjectOfType<AudioManager>();
        audio_m.play("MessageSound");
        //types a letter a frame
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
        //when done, stop the sound
        audio_m.stop("MessageSound");

    }

    /// <summary>
    /// displays the sentences to the user
    /// </summary>
    /// <param name="dialogue">the dialogue to display</param>
    public void StartDailogue(Dialogue dialogue)
    {
        //clear previous diague
        sentences.Clear();
        //make sure the dialogue is shown
        Dialogue.SetBool("IsOpen", true);

        //queue up sentences to be shown
        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }
        //type sentences
        DialogueTitle.text = dialogue.Name;
        DisplayNextSentence();


    }

    //hide dialogue box
    public void EndDialogue()
    {
        Dialogue.SetBool("IsOpen", false);
    }

    /// <summary>
    /// displays the next sentence in the sentences queue
    /// </summary>
    public void DisplayNextSentence()
    {
        // if dialogue is finished, end dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //stops the previous coroutine that is typing
        if (currentDialogue != null)
            StopCoroutine(currentDialogue);
        currentDialogue = StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// types out the message 1 letter a frame
    /// </summary>
    /// <param name="Text">text to type</param>
    /// <returns></returns>
    IEnumerator TypeMessageText(string Text)
    {
        MessageText.text = "";
        //puts a letter in the text field 1 frame at a time
        foreach (char letter in Text.ToCharArray())
        {
            MessageText.text += letter;
            yield return null;
        }
    }

    /// <summary>
    /// starts an event to show to the user
    /// </summary>
    /// <param name="ev"></param>
    public void StartEvent(Event ev)
    {
        //displays the event box
        Message.SetBool("IsOpen", true);

        //types the event
        StartCoroutine(TypeMessageText(ev.Description));
    }

    //closes the event box
    public void EndEvent()
    {
        Message.SetBool("IsOpen", false);
    }
}

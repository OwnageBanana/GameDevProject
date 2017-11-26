using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{


    private Queue<string> sentences;


    public Text DialogueText;
    public Text MessageText;

    public Animator Dialogue;
    public Animator Message;
    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        Debug.Log("DialogueManager Start");
    }

    //types out the sentence in thhe dialogue box
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }

    public void StartDailogue(Dialogue dialogue)
    {
        Debug.Log("playing dialog from the controller");
        sentences.Clear();
        Dialogue.SetBool("IsOpen", true);
        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();


    }

    public void EndDialogue()
    {
        Dialogue.SetBool("IsOpen", false);
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeMessageText(string Text)
    {
        MessageText.text = "";
        foreach (char letter in Text.ToCharArray())
        {
            MessageText.text += letter;
            yield return null;
        }
    }

    public void StartEvent(Dialogue dialogue)
    {
        sentences.Clear();
        Message.SetBool("IsOpen", true);

        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        string text = sentences.Dequeue();
        TypeMessageText(text);

    }

    public void EndMessage()
    {
        Message.SetBool("IsOpen", false);
    }
}

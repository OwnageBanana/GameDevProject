﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;

    public Text DialogueText;
    public Text DialogueTitle;

    public Sprite[] Sprites;
    public float AnimationSpeed;
    public Image Avatar;

    public Text MessageText;
    public Text MessageTitle;

    public Animator Dialogue;
    public Animator Message;



    private Coroutine currentDialogue;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        Debug.Log("DialogueManager Start");
    }

    public IEnumerator AnimateAvatar()
    {
        //destroy all game objects
        for (int i = 0; i < Sprites.Length; i++)
        {
            Avatar.sprite = Sprites[i];
            yield return new WaitForSeconds(AnimationSpeed);
        }
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
        StartCoroutine(AnimateAvatar());
        DialogueTitle.text = dialogue.Name;
        DisplayNextSentence();


    }


    public void EndDialogue()
    {
        StopCoroutine("AnimateAvatar");
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
        //stops the previous coroutine that is typing
        if (currentDialogue != null)
        StopCoroutine(currentDialogue);
        currentDialogue = StartCoroutine(TypeSentence(sentence));
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

    public void StartEvent(Event ev)
    {
        Message.SetBool("IsOpen", true);

        StartCoroutine(TypeMessageText(ev.Description));
    }


    public void EndEvent()
    {
        Message.SetBool("IsOpen", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //talking person's name
    public string Name;
    //the dialogue
    [TextArea(3, 10)]
    public string[] Sentences;
}

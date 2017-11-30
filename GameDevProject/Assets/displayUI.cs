using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class displayUI : MonoBehaviour {
    public string myString;
    public Text myText;
    public float fadeTime;
    public bool displayInfo;

    public GameObject Panel;

    // Use this for initialization
    void Start () {

        myText = GameObject.Find("Text").GetComponent<Text>();
        myText.color = Color.clear;


	}
	
	void Update () {

        FadeText();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.lockCursor = false;

        }

	}
    void OnMouseOver()
    {
        displayInfo = true;
        Panel.gameObject.SetActive(true);
    }
    void OnMouseExit()
    {
        displayInfo = false;
        Panel.gameObject.SetActive(false);
    }
    void FadeText()
    {
        if(displayInfo)
        {
            myText.text = myString;
            myText.color = Color.Lerp(myText.color, Color.white, fadeTime * Time.deltaTime);

        }
        else
        {
            myText.color = Color.Lerp(myText.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }
}

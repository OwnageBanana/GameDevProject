using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickOn : MonoBehaviour {
    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;

    private MeshRenderer myRend;

    [HideInInspector]
    public bool currentlySelected = false;

    public GameObject Panel;
    // Use this for initialization
    void Start () {
        myRend = GetComponent<MeshRenderer>();
        ClickMe();
        

	}


    public void ClickMe()
    {
        if(currentlySelected == false)
        {
            myRend.material = red;
            Panel.gameObject.SetActive(false);
        }
        else
        {
            myRend.material = green;
            Panel.gameObject.SetActive(true);

        }
    }
}

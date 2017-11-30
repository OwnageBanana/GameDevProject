using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{

    public Camera mainCam; // it's not neccessary because we can get our camera from Camera.main

    public Text objectText; // it displays object name

    private CanvasGroup canvasGroup; // we use it to hide canvas
    private GameObject lastSelected = null; // store last selected object reference

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // hide canvas
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if LMB clicked
            RaycastHit raycastHit = new RaycastHit(); // create new raycast hit info object
            Debug.Log("ray on click");
            if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out raycastHit))
            { // create ray from screen's mouse position to world and store info in raycastHit object
                Debug.Log(raycastHit.collider.tag);

                Select(raycastHit.collider.gameObject); // select new cube
            }
        }
    }

    private void Select(GameObject g)
    {
        // when we select cube, we disable rotation script to make it stationary
        Debug.Log(g.ToString());
    }

    private void Deselect(GameObject g)
    {
        if (lastSelected != null)
        { // if we have already selected object
            canvasGroup.alpha = 0f; // make sure to hide canvas; we can hit nothing so we want to disable selection
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {
    [SerializeField]
    private LayerMask clickablesLayer;
	// Use this for initialization
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Raycast on click");
            RaycastHit rayHit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out rayHit, clickablesLayer))
            {
                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();
                clickOnScript.currentlySelected = !clickOnScript.currentlySelected;
                clickOnScript.ClickMe();
            }
        }
	}

}

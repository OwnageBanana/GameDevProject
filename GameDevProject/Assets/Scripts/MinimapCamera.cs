using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour {


    public Transform mainCamera;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void LateUpdate () {


        //setting the x and z of the minimap camera to of the main camera so it follow
        Vector3 newPos = mainCamera.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

	}
}

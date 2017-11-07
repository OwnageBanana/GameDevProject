using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static Resources resources;

	// Use this for initialization
	void Start () {


        //if resources isnt initalized create it
        if (resources == null)
            resources = new Resources();
        //other wise inforce singleton and destroy this object
        else
            Destroy(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

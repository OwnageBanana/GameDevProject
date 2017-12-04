using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolluv : MonoBehaviour {


	void Update () {

        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.GetTextureOffset("_MainTex");


        //Vector2 offset = mat.mainTextureOffset;

        offset.y += Time.deltaTime/10f;

        mat.mainTextureOffset = offset;


	}
}

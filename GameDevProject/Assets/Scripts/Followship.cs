using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followship : MonoBehaviour
{

    public float parralx= 2f;

    void Update()
    {

        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector3 offset = mat.GetTextureOffset("_MainTex");




        offset.x = transform.position.x / transform.localScale.x / parralx;
        offset.y = transform.position.z / 80f * transform.localScale.z / parralx;

        mat.mainTextureOffset = offset;
    }

}
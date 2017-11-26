using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAttribute : MonoBehaviour {

    public Resources gain;
    public Resources cost;

    // Use this for initialization
    void Start()
    {
        gain = new Resources { Energy = 2 };
        cost = new Resources { Garbage = 1 };
    }

}

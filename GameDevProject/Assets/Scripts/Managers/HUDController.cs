using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    public static HUDController instance = null;
    Resources resources;

    public Text food = null;
    public Text happiness = null;
    public Text energy = null;
    public Text shipHP = null;
    public Text garbage = null;
    public Text karma = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        resources = GameObject.FindObjectOfType<ResourceManager>().GetResources();
        RefreshHUD();
    }

    public void RefreshHUD()
    {
        food.text = "" + resources.Food;
        happiness.text = "" + resources.Happiness;
        energy.text = "" + resources.Energy;
        shipHP.text = "" + resources.ShipHp;
        garbage.text = "" + resources.Garbage;
        karma.text = "" + resources.Karma;
    }
}

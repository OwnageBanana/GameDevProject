using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    //static resource object, has resource values
    public static Resources resources;

    //resource caps. may change thrroughout the game
    //note, no happiness cap. not likely needed to change throughout the game
    public int FoodCap = 100;
    public int EnergyCap = 100;
    public int ShipHpCap = 100;
    public int GarbageCap = 100;


    // Use this for initialization
    void Start()
    {
        //if resources isnt initalized create it
        if (resources == null)
            resources = new Resources();
        //other wise inforce singleton and destroy this object
        else
            Destroy(gameObject);

    }


    //removing food. doesnt allow values below zero
    public void removeFood(int amm)
    {
        if (amm > resources.Food)
            resources.Food = 0;
        else
            resources.Food -= amm;
    }

    //Adding food. . doesnt allow values above cap
    public void addFood(int amm)
    {
        if (amm + resources.Food > FoodCap)
            resources.Food = FoodCap;
        else
            resources.Food += amm;
    }

    //removes happiness. doesnt allow values below zero
    public void removeHappiness(int amm)
    {
        if (amm > resources.Happiness)
            resources.Happiness = 0;
        else
            resources.Happiness -= amm;
    }

    //adds happiness. doesnt allow values above 100
    public void addHappiness(int amm)
    {
        if (amm + resources.Happiness > 100)
            resources.Happiness = 100;
        else
            resources.Happiness += amm;
    }

    //removes energy. Doesnt allow values below zero
    public void removeEnergy(int amm)
    {
        if (amm > resources.Food)
            resources.Energy = 0;
        else
            resources.Energy -= amm;
    }

    //adds energy. doesnt allow values above cap (default 100)
    public void addEnergy(int amm)
    {
        if (amm + resources.Happiness > EnergyCap)
            resources.Happiness = EnergyCap;
        else
            resources.Happiness += amm;

    }

    //removes Ship Hp. Doesnt allow values below zero
    public void removeShipHP(int amm)
    {
        if (amm > resources.ShipHp)
            resources.ShipHp = 0;
        else
            resources.ShipHp -= amm;
    }

    //adds Ship Hp. Doesnt allow values above Hp cap
    public void addShipHP(int amm)
    {
        if (amm + resources.ShipHp > ShipHpCap)
            resources.ShipHp = ShipHpCap;
        else
            resources.ShipHp += amm;

    }


}

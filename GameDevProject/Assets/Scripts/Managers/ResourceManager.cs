//Author: Adam Mills


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    //static resource object, has resource values
    public Resources resources;
    public HUDController HUD;

    //resource caps. may change thrroughout the game
    //note, no happiness cap. not likely needed to change throughout the game
    public int FoodCap = 100;
    public int EnergyCap = 100;
    public int ShipHpCap = 100;
    public int GarbageCap = 100;
    public int KarmaCap = 100;

    //Added by Brandon for HUD controller functionality
    public Resources GetResources()
    {
        if (resources == null)
        {
            resources = new Resources();
        }
        return resources;
    }

    // Use this for initialization
    void Start()
    {
        //if resources isnt initalized create it
        if (resources == null)
            resources = new Resources();
        //other wise inforce singleton and destroy this object
        else
            Destroy(gameObject);

        //baseline food level?
        resources.Food = 50;
        resources.Happiness = 100;
        resources.Energy = 100;
        resources.ShipHp = 100;
        resources.Garbage = 0;
        resources.Karma = 0;

    }


    public Resources RemoveResources(Resources toRemove)
    {
        removeFood(toRemove.Food);
        removeHappiness(toRemove.Happiness);
        removeEnergy(toRemove.Energy);
        removeShipHP(toRemove.ShipHp);
        removeKarma(toRemove.Karma);

        HUD.RefreshHUD();
        return resources;

    }


    /// <summary>
    /// adding resources provided to player resources
    /// </summary>
    /// <param name="toAdd">resources to add to player's </param>
    /// <returns>new player resource</returns>
    public Resources AddResources(Resources toAdd)
    {
        addFood(toAdd.Food);
        addHappiness(toAdd.Happiness);
        addEnergy(toAdd.Energy);
        addShipHP(toAdd.ShipHp);
        addKarma(toAdd.Karma);

        AdjustHappiness();

        HUD.RefreshHUD();
        return resources;

    }

    /// <summary>
    /// changes the players resources from provded resources object
    /// </summary>
    /// <param name="toChange">values to change the resources by </param>
    /// <returns>players new resources object</returns>
    public Resources ChangeResources(Resources toChange)
    {
        //changing food
        if (toChange.Food > 0)
            addFood(toChange.Food);
        else
            removeFood(toChange.Food * -1);
        //changing happiness
        if (toChange.Happiness > 0)
            addHappiness(toChange.Happiness);
        else
            removeHappiness(toChange.Happiness * -1);

        //changing energy
        if (toChange.Energy > 0)
            addEnergy(toChange.Energy);
        else
            removeEnergy(toChange.Energy * -1);

        //changing ship HP
        if (toChange.ShipHp > 0)
            addShipHP(toChange.ShipHp);
        else
            removeShipHP(toChange.ShipHp * -1);

        //channging player karma
        if (toChange.Food > 0)
            addKarma(toChange.Karma);
        else
            removeKarma(toChange.Karma * -1);

        AdjustHappiness();

        HUD.RefreshHUD();
        return resources;

    }





    public bool CheckLost()
    {
        bool lost = false;

        lost = resources.Food == 0;
        if (!lost)
            lost = resources.ShipHp == 0;
        if (!lost)
            lost = resources.ShipHp == 0;

        return lost;
    }

    public void AdjustHappiness()
    {
        int toChange = 0;
        if (resources.Food > 50)
            toChange += 1;
        if (resources.Food <= 50)
            toChange -= 1;
        if (resources.Food <= 25)
            toChange -= 1;
        if (resources.Food <= 0)
            toChange -= 1;

        if (resources.Energy <= 50)
            toChange -= 1;
        if (resources.Energy <= 25)
            toChange -= 1;
        if (resources.Energy <= 0)
            toChange -= 1;


        if (resources.Food > 50)
            toChange -= 1;
        if (resources.Garbage <= 50)
            toChange += 1;
        if (resources.Energy <= 25)
            toChange += 1;
        if (resources.Energy <= 0)
            toChange += 1;


        if (toChange > 0)
            addHappiness(toChange);
        else
            removeHappiness(toChange);
    }


    /*Getters and setter of the resource values*/

    //removing food. doesnt allow values below zero
    public void removeFood(int amm)
    {
        if (amm > resources.Food)
            resources.Food = 0;
        else
            resources.Food -= amm;
        HUD.RefreshHUD();
    }

    //Adding food. . doesnt allow values above cap
    public void addFood(int amm)
    {
        if (amm + resources.Food > FoodCap)
            resources.Food = FoodCap;
        else
            resources.Food += amm;
        HUD.RefreshHUD();
    }

    //removes happiness. doesnt allow values below zero
    public void removeHappiness(int amm)
    {
        if (amm > resources.Happiness)
            resources.Happiness = 0;
        else
            resources.Happiness -= amm;
        HUD.RefreshHUD();
    }

    //adds happiness. doesnt allow values above 100
    public void addHappiness(int amm)
    {
        if (amm + resources.Happiness > 100)
            resources.Happiness = 100;
        else
            resources.Happiness += amm;
        HUD.RefreshHUD();
    }

    //removes energy. Doesnt allow values below zero
    public void removeEnergy(int amm)
    {
        if (amm > resources.Food)
            resources.Energy = 0;
        else
            resources.Energy -= amm;
        HUD.RefreshHUD();
    }

    //adds energy. doesnt allow values above cap (default 100)
    public void addEnergy(int amm)
    {
        if (amm + resources.Happiness > EnergyCap)
            resources.Happiness = EnergyCap;
        else
            resources.Happiness += amm;
        HUD.RefreshHUD();
    }

    //removes Ship Hp. Doesnt allow values below zero
    public void removeShipHP(int amm)
    {
        if (amm > resources.ShipHp)
            resources.ShipHp = 0;
        else
            resources.ShipHp -= amm;
        HUD.RefreshHUD();
    }

    //adds Ship Hp. Doesnt allow values above Hp cap
    public void addShipHP(int amm)
    {
        if (amm + resources.ShipHp > ShipHpCap)
            resources.ShipHp = ShipHpCap;
        else
            resources.ShipHp += amm;
        HUD.RefreshHUD();
    }


    //removes karma. Doesnt allow values below - karma cap
    public void removeKarma(int amm)
    {
        if (resources.Karma - amm <= (KarmaCap * -1))
            resources.Karma = (KarmaCap * -1);
        else
            resources.Karma -= amm;
        HUD.RefreshHUD();
    }

    //adds Ship Hp. Doesnt allow values above karma cap
    public void addKarma(int amm)
    {
        if (amm + resources.Karma > KarmaCap)
            resources.Karma = KarmaCap;
        else
            resources.ShipHp += amm;
        HUD.RefreshHUD();
    }
}

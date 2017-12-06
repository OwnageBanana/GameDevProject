//Author: Adam Mills

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{



    public Text RoomName;

    public Text FoodGain;
    public Text HappinessGain;
    public Text EnergyGain;
    public Text GarbageGain;

    public Text FoodCost;
    public Text HappinessCost;
    public Text EnergyCost;
    public Text GarbageCost;

    public Toggle Enabled;

    public GameObject Panel;
    public RoomAttribute selectedRoom;

    //Update is called once per frame
    public void DisplayMenu(RoomAttribute room)
    {

        Panel.SetActive(true);

        selectedRoom = room;

        RoomName.text = room.Room;

        FoodGain.text = room.gain.Food.ToString();
        HappinessGain.text = room.gain.Happiness.ToString();
        EnergyGain.text = room.gain.Energy.ToString();
        GarbageGain.text = room.gain.Garbage.ToString();

        FoodCost.text = room.cost.Food.ToString();
        HappinessCost.text = room.cost.Happiness.ToString();
        EnergyCost.text = room.cost.Energy.ToString();
        GarbageCost.text = room.cost.Garbage.ToString();

        Enabled.isOn = room.roomEnabled;
    }

    public void HideMenu()
    {
        Panel.SetActive(false);
    }

}

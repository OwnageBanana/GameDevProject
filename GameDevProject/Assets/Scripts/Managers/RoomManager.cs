//Author: Adam Mills

///UI Manager for the room menu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{

    //menu title for room name
    public Text RoomName;

    //text boxes for the gained resources
    public Text FoodGain;
    public Text HappinessGain;
    public Text EnergyGain;
    public Text GarbageGain;

    //text boxes for the lost resources
    public Text FoodCost;
    public Text HappinessCost;
    public Text EnergyCost;
    public Text GarbageCost;

    //ui element to show if  a room is enabled
    public Toggle Enabled;

    public GameObject Panel;
    public RoomAttribute selectedRoom;

    /// <summary>
    /// called when a room is clicked on
    /// </summary>
    /// <param name="room">the attribute object of hte room clicked on</param>
    public void DisplayMenu(RoomAttribute room)
    {


        //on displaying the menu
        Panel.SetActive(true);

        //setting selected room
        selectedRoom = room;

        //putting room attribute values into the ui
        RoomName.text = room.Room;

        FoodGain.text = room.gain.Food.ToString();
        HappinessGain.text = room.gain.Happiness.ToString();
        EnergyGain.text = room.gain.Energy.ToString();
        GarbageGain.text = room.gain.Garbage.ToString();

        FoodCost.text = room.cost.Food.ToString();
        HappinessCost.text = room.cost.Happiness.ToString();
        EnergyCost.text = room.cost.Energy.ToString();
        GarbageCost.text = room.cost.Garbage.ToString();

        //setting toggle element to show if the room is enabled
        Enabled.isOn = room.roomEnabled;
    }

    /// <summary>
    /// hides the menu when the x is clicked on
    /// </summary>
    public void HideMenu()
    {
        Panel.SetActive(false);
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;
using System.Xml.Serialization;
using System.Xml.Linq;

public class EventManager : MonoBehaviour {

    List<Event> Events = new List<Event>();

    // Use this for initialization, called on script enabling
    void Start()
    {

        try
        {
            //initialize serializer to use List<Event>
            var serializer = new XmlSerializer(typeof(List<Event>));
            //get the plain text from Events.xml file
            string xml = File.ReadAllText("Assets/TextResources/Events.xml");
            //parse the xml to remove white space and special characters
            xml = XDocument.Parse(xml).ToString(SaveOptions.DisableFormatting);

            //read in the xml to be deserialized
            using (var stringReader = new StringReader(xml))
            {
                using (var reader = XmlReader.Create(stringReader))
                {
                    //deserialize the xml
                    var result = (List<Event>)serializer.Deserialize(reader);
                    Events = result;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }



    }

    public Event PickEvent(Resources resources)
    {
        Event choice = new Event();

        //based on some of the player stats we can decide the type of event that they get.

        //maybe, if they are damaged, some kind vessel will offer aid, some evil alien will try to take advantage of that,etc.,
        if (resources.ShipHp < 50)
            ;
        //happy Crew? they do some thing smart
        if (resources.Happiness > 75)
            ;
        //sad crew, builds rec room
        else if (resources.Happiness > 50)
            ;
        //mad crew, damages ship
        else if (resources.Happiness > 25)
            ;
        //positive Karma? good stuff more likely to happen
        if (resources.Karma > 0)
            ;
        //negative Karma? bad stuff more likely to happen
        if (resources.Karma < 0)
            ;


        return choice;
    }









    private void serializeList()
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<Event>));
        List<Event> list = new List<Event>();
        list.Add(new Event { EventId = 1, Description = "abc", EventType = EventType.Good, Cost = new Resources { Food = 0, Energy = 1, Happiness = 2, ShipHp = 3, Garbage = 4 }, Reward = new Resources { Food = 0, Energy = 1, Happiness = 2, ShipHp = 3, Garbage = 4 } });
        list.Add(new Event { EventId = 2, Description = "def", EventType = EventType.Good, Cost = new Resources { Food = 0, Energy = 1, Happiness = 2, ShipHp = 3, Garbage = 4 }, Reward = new Resources { Food = 0, Energy = 1, Happiness = 2, ShipHp = 3, Garbage = 4 } });
        list.Add(new Event { EventId = 3, Description = "ghi", EventType = EventType.Good, Cost = new Resources { Food = 0, Energy = 1, Happiness = 2, ShipHp = 3, Garbage = 4 }, Reward = new Resources { Food = 0, Energy = 1, Happiness = 2, ShipHp = 3, Garbage = 4 } });
        StreamWriter sw = new StreamWriter("Assets/TextResources/NewEvents.xml");
        ser.Serialize(sw, list);
        sw.Close();
    }
}


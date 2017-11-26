//Author: Adam Mills

using System;
using System.Xml.Serialization;
using UnityEngine;

[Serializable, XmlRoot("Resources")]
public class Resources
{
    //Each prop can be extended to food being a Food object with expiry, etc
    [XmlElement("Food")]
    public int Food;// { get; set; } // 0 to cap
    [XmlElement("Happiness")]
    public int Happiness;// { get; set; } // 0 to 100
    [XmlElement("Karma")]
    public int Karma;// { get; set; } // -100 to 100
    [XmlElement("Energy")]
    public int Energy;// { get; set; } //0 to cap
    [XmlElement("ShipHp")]
    public int ShipHp;// { get; set; }// cap to 0
    [XmlElement("Garbage")]
    public int Garbage;// { get; set; } // 0 to cap
}

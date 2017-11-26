//Author: Adam Mills

using System;
using System.Xml.Serialization;


    [XmlType("EventType")]
    public enum EventType
    {
        [XmlEnum("Good")]
        Good = 0,
        [XmlEnum("Bad")]
        Bad,
        [XmlEnum("Neutral")]
        Neutral
    }

//Author: Adam Mills
using System.Xml.Serialization;
using System;

[Serializable, XmlRoot("Event")]
public class Event
{
    [XmlElement("Description")]
    public string Description { get; set; }

    [XmlElement("Reward")]
    public Resources Reward { get; set; }

    [XmlElement("Cost")]
    public Resources Cost { get; set; }

    [XmlElement("EventId")]
    public int EventId { get; set; }

    [XmlElement("EventType")]
    public EventType EventType { get; set; }
}

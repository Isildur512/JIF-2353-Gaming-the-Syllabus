using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System;


[XmlRoot("PowerUp")]
public class PowerUp : IXmlSerializable
{

    public string powerUpName {get; protected set;}
    public string dialogueColor {get; protected set;}
    public TargetType Target { get; protected set; } = TargetType.None;
    // public TargetType Target { get; protected set; } = TargetType.None;
    /// <summary>
    /// The behavior to apply to the given targets when the effect occurs.
    /// </summary>
    public virtual void Apply(CombatUnit target) {

    }

    /// <summary>
    /// When overwriting this method, you should always call the base method and only write the additional attributes
    /// needed for your child class. Do not open or close any elements; that is handled in <see cref="UnitAction"/>.
    /// </summary>
    public virtual void WriteXml(XmlWriter writer)
    {
        // writer.WriteAttributeString("type", GetType().Name);
        writer.WriteAttributeString("name", powerUpName);
        writer.WriteAttributeString("dialogueColor", dialogueColor);
        writer.WriteAttributeString("target", Target.ToString());
    }

    public virtual void ReadXml(XmlReader reader)
    {
        powerUpName = reader.GetAttribute("name"); 
        Target = Enum.Parse<TargetType>(XmlUtilities.GetAttributeOrDefault(reader, "target", "None"));
        dialogueColor = reader.GetAttribute("dialogueColor");
        Debug.Log("THIS WAS HIT");
        // DelayInSecondsBeforeEffects = float.Parse(XmlUtilities.GetAttributeOrDefault(reader, "delayInSecondsBeforeEffects", "0.0"));
        // DelayInSecondsAfterEffects = float.Parse(XmlUtilities.GetAttributeOrDefault(reader, "delayInSecondsAfterEffects", "0.0"));
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }
}

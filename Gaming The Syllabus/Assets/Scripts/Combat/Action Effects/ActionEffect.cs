using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System;

public abstract class ActionEffect : IXmlSerializable
{
    public TargetType Target { get; protected set; } = TargetType.None;

    public float DelayInSecondsBeforeEffects { get; private set; }
    public float DelayInSecondsAfterEffects { get; private set; }

    /// <summary>
    /// The behavior to apply to the given targets when the effect occurs.
    /// </summary>
    public abstract void Apply(params CombatUnit[] targets);

    /// <summary>
    /// When overwriting this method, you should always call the base method and only write the additional attributes
    /// needed for your child class. Do not open or close any elements; that is handled in <see cref="UnitAction"/>.
    /// </summary>
    public virtual void WriteXml(XmlWriter writer)
    {
        writer.WriteAttributeString("type", GetType().Name);
        writer.WriteAttributeString("target", Target.ToString());
        writer.WriteAttributeString("delayInSecondsBeforeEffects", DelayInSecondsBeforeEffects.ToString());
        writer.WriteAttributeString("delayInSecondsAfterEffects", DelayInSecondsAfterEffects.ToString());
    }

    public virtual void ReadXml(XmlReader reader)
    {
        Target = Enum.Parse<TargetType>(XmlUtilities.GetAttributeOrDefault(reader, "target", "None"));
        DelayInSecondsBeforeEffects = float.Parse(XmlUtilities.GetAttributeOrDefault(reader, "delayInSecondsBeforeEffects", "0.0"));
        DelayInSecondsAfterEffects = float.Parse(XmlUtilities.GetAttributeOrDefault(reader, "delayInSecondsAfterEffects", "0.0"));
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System;

public abstract class ActionEffect : IXmlSerializable
{
    [SerializeField] protected TargetType _target = TargetType.None;
    public TargetType Target { get => _target; }

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
        writer.WriteAttributeString("target", _target.ToString());
    }

    public virtual void ReadXml(XmlReader reader)
    {
        // Default to None if no target is specified in the XML
        if (!string.IsNullOrEmpty(reader.GetAttribute("target")))
        {
            _target = (TargetType)Enum.Parse(typeof(TargetType), reader.GetAttribute("target"));
        } else
        {
            _target = TargetType.None;
        }
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }
}

/// <summary>
/// This needs to contain every ActionEffect that exists. When you create new ones, make sure to add them here and to AllActionEffects.
/// </summary>
public enum ActionEffects
{
    DamageTarget,
    LogMessage
}

/// <summary>
/// This needs to contain every ActionEffect that exists. When you create new ones, make sure to add them here and to ActionEffects.
/// </summary>
public static class AllActionEffects
{
    public static Type GetActionEffect(this ActionEffects effect) => effect switch
    {
        ActionEffects.DamageTarget => typeof(DamageTarget),
        ActionEffects.LogMessage => typeof(LogMessage),
        _ => throw new NotImplementedException(),
    };
}

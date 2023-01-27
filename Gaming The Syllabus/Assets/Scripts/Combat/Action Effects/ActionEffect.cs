using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System;

public abstract class ActionEffect : IXmlSerializable
{
    [SerializeField] protected TargetType _target;
    [SerializeField] protected int _damageAmount;
    public TargetType Target { get => _target; }
    public int DamageAmount { get => _damageAmount; }

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
        _target = (TargetType)Enum.Parse(typeof(TargetType), reader.GetAttribute("target"));
        _damageAmount = int.Parse(reader.GetAttribute("damageAmount"));
    }

    public XmlSchema GetSchema()
    {
        return (null);
    }
}

public enum ActionEffects
{
    DamageTarget
}

public static class AllActionEffects
{
    public static Type GetActionEffect(this ActionEffects effect) => effect switch
    {
        ActionEffects.DamageTarget => typeof(DamageTarget),
        _ => throw new NotImplementedException(),
    };
}

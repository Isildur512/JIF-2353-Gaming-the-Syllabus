using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

public class DamageTarget : ActionEffect
{
    private int damageAmount;

    public DamageTarget(int damageAmount, TargetType target)
    {
        this.damageAmount = damageAmount;
        _target = target;
    }

    public DamageTarget()
    {

    }

    public override void Apply(params CombatUnit[] targets)
    {
        foreach (CombatUnit target in targets)
        {
            target.ApplyDamage(damageAmount);
        }
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);
        writer.WriteAttributeString("damageAmount", damageAmount.ToString());
    }

    public override void ReadXml(XmlReader reader)
    {
        base.ReadXml(reader);
        damageAmount = int.Parse(reader.GetAttribute("damageAmount"));
    }
}

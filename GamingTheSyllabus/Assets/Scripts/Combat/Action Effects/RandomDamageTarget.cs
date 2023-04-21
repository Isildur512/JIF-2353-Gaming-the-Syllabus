using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

public class RandomDamageTarget : ActionEffect
{
    public int minDamage { get; private set; }
    public int maxDamage { get; private set; }

    public RandomDamageTarget(int minDamage, int maxDamage, TargetType target)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        Target = target;
    }

    public RandomDamageTarget()
    {

    }

    public override void Apply(params CombatUnit[] targets)
    {
        foreach (CombatUnit target in targets)
        {
            int damageAmount = calculateDamageAmount(minDamage, maxDamage);
            target.ApplyDamage(damageAmount);
            CombatUnit attacker = CombatManager.currentCombatant;
            DialogueBoxUIManager.AddStringToDialogueBox
            ($"{DialogueBoxUIManager.FormatCombatUnitColor(attacker)}"
            + $" dealt {DialogueBoxUIManager.FormatDamageColor(damageAmount)} damage to "
            + $"{DialogueBoxUIManager.FormatCombatUnitColor(target)}");
        }
    }

    private int calculateDamageAmount(int minDamage, int maxDamage)
    {
        System.Random randomNum = new System.Random();
        return randomNum.Next(minDamage, maxDamage + 1);
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);
        writer.WriteAttributeString("minDamage", minDamage.ToString());
        writer.WriteAttributeString("maxnDamage", maxDamage.ToString());
    }

    public override void ReadXml(XmlAttributeCollection attributes)
    {
        base.ReadXml(attributes);
        minDamage = int.Parse(attributes["minDamage"].Value);
        maxDamage = int.Parse(attributes["maxDamage"].Value);
    }
}

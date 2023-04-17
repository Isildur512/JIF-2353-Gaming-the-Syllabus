using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

public class RandomHealTarget : ActionEffect
{
    public int minHeal { get; private set; }
    public int maxHeal { get; private set; }

    public RandomHealTarget(int minHeal, int maxHeal, TargetType target)
    {
        this.minHeal = minHeal;
        this.maxHeal = maxHeal;
        Target = target;
    }

    public RandomHealTarget()
    {

    }

    public override void Apply(params CombatUnit[] targets)
    {
        CombatUnit caller = CombatManager.currentCombatant;
        foreach (CombatUnit target in targets)
        {
            int healAmount = calculateHealAmount(minHeal, maxHeal);
            target.HealUnit(healAmount);
            DialogueBoxUIManager.AddStringToDialogueBox
            ($"{DialogueBoxUIManager.FormatCombatUnitColor(caller)}"
            + $"{DialogueBoxUIManager.FormatDamageColor(healAmount)}");
        }
    }

    private int calculateHealAmount(int minHeal, int maxHeal)
    {
        System.Random randomNum = new System.Random();
        return randomNum.Next(minHeal, maxHeal + 1);
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);
        writer.WriteAttributeString("minHeal", minHeal.ToString());
        writer.WriteAttributeString("maxHeal", maxHeal.ToString());
    }

    public override void ReadXml(XmlAttributeCollection attributes)
    {
        base.ReadXml(attributes);
        minHeal = int.Parse(attributes["minHeal"].Value);
        maxHeal = int.Parse(attributes["maxHeal"].Value);
    }
}

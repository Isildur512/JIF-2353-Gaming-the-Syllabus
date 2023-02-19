using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

public class HealTarget : ActionEffect
{
    public int healAmount;

    public HealTarget(int healAmount, TargetType target)
    {
        this.healAmount = healAmount;
        Target = target;
    }

    public HealTarget()
    {

    }

    public override void Apply(params CombatUnit[] targets)
    {

        CombatUnit caller = CombatManager.currentCombatant;

        foreach (CombatUnit target in targets)
        {

            if (AbilityCaller != null) // If this is not null, then this means a PlayerAbility is using this effect.
            {
                healAmount = AbilityCaller.calculateDamage(AbilityCaller.AbilityNode);
                caller.HealUnit(healAmount);
            } 
            else
            {           
                target.HealUnit(healAmount);
            }

            DialogueBoxUIManager.AddStringToDialogueBox
            ($"{DialogueBoxUIManager.FormatCombatUnitColor(caller)}"
            + $"{DialogueBoxUIManager.FormatDamageColor(healAmount)}");
        }
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);
        writer.WriteAttributeString("healAmount", healAmount.ToString());
    }

    public override void ReadXml(XmlReader reader)
    {
        base.ReadXml(reader);
        healAmount = int.Parse(reader.GetAttribute("healAmount"));
    }

}

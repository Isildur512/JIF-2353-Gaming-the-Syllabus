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
        foreach (CombatUnit target in targets)
        {
            target.HealUnit(healAmount);
            CombatUnit caller = CombatManager.currentCombatant;
            DialogueBoxUIManager.AddStringToDialogueBox
            ($"<color=\"{caller.dialogueColor}\">{caller.UnitName}</color>"
            + $" rested and healed for <color=\"green\">+{healAmount}</color>");
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

using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

public class DamageTarget : ActionEffect
{
    public int damageAmount { get; private set; }

    public DamageTarget(int damageAmount, TargetType target)
    {
        this.damageAmount = damageAmount;
        Target = target;
    }

    public DamageTarget()
    {

    }

    public override void Apply(params CombatUnit[] targets)
    {
        foreach (CombatUnit target in targets)
        {
            if (AbilityCaller != null)
                damageAmount = AbilityCaller.calculateDamage(AbilityCaller.AbilityNode);
                
            target.ApplyDamage(damageAmount);
            CombatUnit attacker = CombatManager.currentCombatant;
            DialogueBoxUIManager.AddStringToDialogueBox
            ($"{DialogueBoxUIManager.FormatCombatUnitColor(attacker)}"
            + $" dealt {DialogueBoxUIManager.FormatDamageColor(-damageAmount)} damage to "
            + $"{DialogueBoxUIManager.FormatCombatUnitColor(target)}");
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
        Debug.Log($"Damage Amount: {damageAmount}");
    }

}

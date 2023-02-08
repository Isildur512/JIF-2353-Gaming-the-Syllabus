using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

public class DamagePowerUp : PowerUp
{
    public int damageIncreaseAmt;

    public DamagePowerUp(int damageIncreaseAmt, TargetType target)
    {
        this.damageIncreaseAmt = damageIncreaseAmt;
        this.Target = Target;
    }

    public DamagePowerUp()
    {
        XmlUtilities.Deserialize<PowerUp>("XML/Powerups.xml");
    }

    public override void Apply(CombatUnit target)
    {
        foreach (UnitAction action in target.actions) {
            foreach (ActionEffect effect in action.effects) {
                if (effect.GetType() == typeof(DamageTarget)) {
                    ((DamageTarget)effect).damageAmount += damageIncreaseAmt;
                }
            }
        }
        Debug.Log(this.powerUpName);
        // DialogueBoxUIManager.addStringToDialogueBox($"<color=\"{target.dialogueColor}\">{target.UnitName}</color>"
        //     + $" used <color=\"{this.dialogueColor}\" {this.powerUpName}</color>");
        // foreach (CombatUnit target in targets)
        // {
        //     target.ApplyDamage(damageAmount);
        //     CombatUnit attacker = CombatManager.getCurrentCombatant();
        //     DialogueBoxUIManager.addStringToDialogueBox
        //     ($"<color=\"{attacker.dialogueColor}\">{attacker.UnitName}</color>"
        //     + $" dealt <color=\"red\">{damageAmount}</color> damage to <color=\"{target.dialogueColor}\">{target.UnitName}!</color>");
        // }
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);
        writer.WriteAttributeString("damageIncreaseAmt", damageIncreaseAmt.ToString());
    }

    public override void ReadXml(XmlReader reader)
    {
        base.ReadXml(reader);
        damageIncreaseAmt = int.Parse(reader.GetAttribute("damageIncreaseAmt"));
        Debug.Log("THIS WAS HIT");
    }
}

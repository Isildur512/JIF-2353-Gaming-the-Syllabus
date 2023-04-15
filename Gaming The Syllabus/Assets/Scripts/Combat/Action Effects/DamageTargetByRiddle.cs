using System.Threading.Tasks;
using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

public class DamageTargetByRiddle : ActionEffect
{
    public int minDamage { get; private set; }
    public int maxDamage { get; private set; }

    public DamageTargetByRiddle(int minDamage, int maxDamage, TargetType target)
    {
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        Target = target;
    }

    public DamageTargetByRiddle()
    {

    }

    public override void Apply(params CombatUnit[] targets)
    {
        foreach (CombatUnit target in targets)
        {
            CombatUnit attacker = CombatManager.currentCombatant;
            int damageAmount = calculateDamageAmount(minDamage, maxDamage);
            target.ApplyDamage(damageAmount);
            DialogueBoxUIManager.AddStringToDialogueBox
            ($"{DialogueBoxUIManager.FormatCombatUnitColor(attacker)}"
            + $" dealt {DialogueBoxUIManager.FormatDamageColor(damageAmount)} damage to "
            + $"{DialogueBoxUIManager.FormatCombatUnitColor(target)}");
        }
    }

    public static async Task<bool> HandleRiddleResult() {
        await Task.Delay(500);
        System.Random randomNum = new System.Random();
        int riddleIndex = randomNum.Next(0, SyllabusRiddleManager.Riddles.Length);
        SyllabusRiddleUIManager.DisplayRiddle(riddleIndex);
        while (AnswerSubmissionManager.isSubmitted == false) {
            await Task.Delay(100);
        }
        AnswerSubmissionManager.isSubmitted = false;
        return AnswerSubmissionManager.isCorrect;

    }

    private int calculateDamageAmount(int minDamage, int maxDamage)
    {
        System.Random randomNum = new System.Random();
        if (AnswerSubmissionManager.isCorrect) {
            return randomNum.Next(minDamage, maxDamage / 2);
        } else {
            return randomNum.Next(minDamage, maxDamage);
        }
    }

    public override void WriteXml(XmlWriter writer)
    {
        base.WriteXml(writer);
        writer.WriteAttributeString("minDamage", minDamage.ToString());
        writer.WriteAttributeString("maxDamage", maxDamage.ToString());
    }

    public override void ReadXml(XmlReader reader)
    {
        base.ReadXml(reader);
        minDamage = int.Parse(reader.GetAttribute("minDamage"));
        maxDamage = int.Parse(reader.GetAttribute("maxDamage"));

    }
}

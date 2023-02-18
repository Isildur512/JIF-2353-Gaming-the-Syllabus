using System;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;


[System.Serializable]
public class PlayerAbility
{ 
    [SerializeField] private string Name;
    public string AbilityName { get => Name; private set => Name = value; }
    public string AbilityType { get; private set; }
    public bool IsDefaultAbility { get; private set; }
    public string AbilityDesc { get; private set; }
    public XmlNode AbilityNode { get; private set; }

    public List<ActionEffect> AbilityActionEffects { get; private set; }


    public PlayerAbility(XmlNode curItemNode) 
    {
        AbilityName = curItemNode.Attributes["name"].Value;
        AbilityType = curItemNode.Attributes["type"].Value;
        IsDefaultAbility = bool.Parse(curItemNode.Attributes["default"].Value);
        AbilityDesc = curItemNode["AbilityDesc"].InnerText;
        AbilityNode = curItemNode;

        AbilityActionEffects = new List<ActionEffect>();

        foreach(XmlNode effect in AbilityNode["Effects"])
        {
            ActionEffects effectType = Enum.Parse<ActionEffects>(effect.Attributes["type"].Value);
            ActionEffect newEffect = (ActionEffect)Activator.CreateInstance(AllActionEffects.GetActionEffect(effectType));
            newEffect.AbilityCaller = this;
            AbilityActionEffects.Add(newEffect);
        }
    }


    public void PerformAttackAbility(params CombatUnit[] targets)
    {
        CombatUnit attacker = CombatManager.currentCombatant;
        if (calculateHitChance(AbilityNode)) {
            foreach (CombatUnit target in targets)
            {
                if (!target.IsAlive) {
                    continue;
                }
                
                foreach(ActionEffect effect in AbilityActionEffects)
                {
                    effect.Apply(target);
                }

                int damageAmt = calculateDamage(AbilityNode);
                // target.ApplyDamage(damageAmt);
                // DialogueBoxUIManager.AddStringToDialogueBox($"{attacker.UnitName} {AbilityNode["hitMessage"].InnerText} {damageAmt} damage to {target.UnitName}");
            }
        }
        else 
        {
            DialogueBoxUIManager.AddStringToDialogueBox($"{attacker.UnitName} {AbilityNode["missMessage"].InnerText}");
        }
    }


    public bool calculateHitChance(XmlNode ability)
    {
        System.Random random = new System.Random();
        int rollChance = random.Next(0, 100);
        return int.Parse(ability["hitChance"].InnerText) >= rollChance;
    }


    public int calculateDamage(XmlNode ability)
    {
        System.Random num = new System.Random();
        return num.Next(int.Parse(ability["minDamage"].InnerText), int.Parse(ability["maxDamage"].InnerText));
    }
}
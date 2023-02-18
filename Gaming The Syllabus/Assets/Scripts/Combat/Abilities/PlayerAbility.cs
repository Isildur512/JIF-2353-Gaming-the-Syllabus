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


    public PlayerAbility(XmlNode curItemNode) 
    {
        AbilityName = curItemNode.Attributes["name"].Value;
        AbilityType = curItemNode.Attributes["type"].Value;
        IsDefaultAbility = bool.Parse(curItemNode.Attributes["default"].Value);
        AbilityDesc = curItemNode["AbilityDesc"].InnerText;
        AbilityNode = curItemNode;
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
                
                int damageAmt = calculateDamage(AbilityNode);
                target.ApplyDamage(damageAmt);
                DialogueBoxUIManager.AddStringToDialogueBox($"{attacker.UnitName} {AbilityNode["hitMessage"].InnerText} {damageAmt} damage to {target.UnitName}");
            }
        }
        else 
        {
            DialogueBoxUIManager.AddStringToDialogueBox($"{attacker.UnitName} {AbilityNode["missMessage"].InnerText}");
        }
    }


    private bool calculateHitChance(XmlNode ability)
    {
        int rollChance = Random.Range(0, 100);
        return int.Parse(ability["hitChance"].InnerText) >= rollChance;
    }


    private int calculateDamage(XmlNode ability)
    {
        return Random.Range(int.Parse(ability["minDamage"].InnerText), int.Parse(ability["maxDamage"].InnerText));
    }

}
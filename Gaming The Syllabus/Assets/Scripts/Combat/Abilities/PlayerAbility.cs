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
    public string abilityName { get => Name; private set => Name = value; }
    public string abilityType { get; private set; }
    public bool isDefaultAbility { get; private set; }
    public string abilityDesc { get; private set; }
    public XmlNode abilityNode { get; private set; }

    public PlayerAbility(XmlNode curItemNode) 
    {
        abilityName = curItemNode.Attributes["name"].Value;
        abilityType = curItemNode.Attributes["type"].Value;
        isDefaultAbility = bool.Parse(curItemNode.Attributes["default"].Value);
        abilityDesc = curItemNode["AbilityDesc"].InnerText;
        abilityNode = curItemNode;
        Debug.Log($"{abilityName} {abilityType}");
    }


    public void PerformAttackAbility(params CombatUnit[] targets)
    {
        CombatUnit attacker = CombatManager.getCurrentCombatant();
        foreach (CombatUnit target in targets)
        {
            if (calculateHitChance(abilityNode))
            {
                int damageAmt = calculateDamage(abilityNode);
                target.ApplyDamage(damageAmt);
                DialogueBoxUIManager.addStringToDialogueBox($"{attacker.UnitName} {abilityNode["hitMessage"]} {damageAmt} damage to {target.UnitName}");
            } else {
                DialogueBoxUIManager.addStringToDialogueBox($"{attacker.UnitName} {abilityNode["missMessage"]}");
            }
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
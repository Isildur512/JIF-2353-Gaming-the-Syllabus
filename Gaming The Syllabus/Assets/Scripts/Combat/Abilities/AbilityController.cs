using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using UnityEngine.Events;

public class AbilityController : MonoBehaviour
{

    public static XmlDocument abilityDataXml = new XmlDocument();
    public TextAsset abilitiesXML;

    
    public List<PlayerAbility> allAbilities;
    private void Awake()
    {
        abilityDataXml.LoadXml(abilitiesXML.text);
        allAbilities = new List<PlayerAbility>();
        FindAllAbilities();
    }

    public void FindAllAbilities()
    {
        XmlNodeList abilities = abilityDataXml.SelectNodes("/PlayerAbilities/PlayerAbility");
        foreach (XmlNode ability in abilities)
        {
            allAbilities.Add(new PlayerAbility(ability));
        }
    }


    public static void GiveDefaultAbilities(CombatUnit target)
    {
        XmlNodeList abilities = abilityDataXml.SelectNodes("/PlayerAbilities/PlayerAbility[@default='true']");
        foreach (XmlNode ability in abilities)
        {
            Debug.Log($"Abilities length: {abilities.Count}");
            PlayerAbility newAbility = new PlayerAbility(ability);
            CombatManager.player.abilities.Add(newAbility);
        }
    }


    // This is case sensitive for abilityName. Be sure to pass in the string as the "name" attribute within PlayerAbilites.xml
    public static void GiveAbility(CombatUnit target, string abilityName)
    {
        foreach (PlayerAbility ability in target.abilities)
        {
            if (abilityName == ability.abilityName)
            {
                Debug.Log($"{target.UnitName} already has {abilityName}");
                return;
            }
        }

        XmlNodeList abilityNode = abilityDataXml.SelectNodes($"/PlayerAbilities/PlayerAbility");
        foreach (XmlNode ability in abilityNode)
        {
            if (ability.Attributes["name"].Value == abilityName)
            {
                PlayerAbility newAbility = new PlayerAbility(ability);
                target.abilities.Add(newAbility);
                AbilityPageUIManager.AppendAbilityToPage(newAbility);
            }
            else
            {
                Debug.Log($"Could not find {abilityName} in PlayerAbilities.xml!");
            }
        }
    }


    public static void UseAttackAbility(PlayerAbility ability)
    {
        if (CombatManager.getCurrentCombatant().UnitName == "Player")
        {
            ability.PerformAttackAbility(CombatManager.GetTargetsByType(TargetType.AllEnemies));
            CombatManager.NextTurn();
        }
    }
}

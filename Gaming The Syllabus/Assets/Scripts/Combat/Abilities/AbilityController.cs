using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using UnityEngine.Events;

public class AbilityController : MonoBehaviour
{

    [SerializeField] private TextAsset abilitiesXML;

    
    private static List<PlayerAbility> allAbilities;
    private void Awake()
    {
        allAbilities = new List<PlayerAbility>();
        LoadAllAbilities();
        Debug.Log($"ability count: {allAbilities.Count}");
    }


    /// This adds every ability from PlayerAbilities to the allAbilities list. 
    public void LoadAllAbilities()
    {
        XmlDocument abilityDataXml = new XmlDocument();
        abilityDataXml.LoadXml(abilitiesXML.text);

        XmlNodeList abilities = abilityDataXml.SelectNodes("/PlayerAbilities/PlayerAbility");
        foreach (XmlNode ability in abilities)
        {
            allAbilities.Add(new PlayerAbility(ability));
        }
    }


    public static void GiveDefaultAbilities(CombatUnit target)
    {
        // XmlNodeList abilities = abilityDataXml.SelectNodes("/PlayerAbilities/PlayerAbility[@default='true']");
        foreach (PlayerAbility ability in allAbilities)
        {
            if (ability.IsDefaultAbility)
            CombatManager.player.abilities.Add(ability);
        }
    }


    /// This is case sensitive for abilityName. Be sure to pass in the string as the "name" attribute within PlayerAbilites.xml
    public static void GiveAbility(CombatUnit target, string abilityName)
    {
        foreach (PlayerAbility ability in target.abilities)
        {
            if (abilityName == ability.AbilityName)
            {
                Debug.Log($"{target.UnitName} already has {abilityName}");
                return;
            }
        }

        foreach (PlayerAbility ability in allAbilities)
        {
            if (ability.AbilityName == abilityName)
            {
                target.abilities.Add(ability);
                AbilityPageUIManager.AppendAbilityToPage(ability);
            }
            else
            {
                Debug.Log($"Could not find {abilityName} in PlayerAbilities.xml!");
            }
        }
    }


    public static void UseAttackAbility(PlayerAbility ability)
    {
        if (CombatManager.IsPlayerTurn)
        {
            ability.PerformAttackAbility(CombatManager.GetTargetsByType(TargetType.AllEnemies));
            CombatManager.NextTurn();
        }
    }
}
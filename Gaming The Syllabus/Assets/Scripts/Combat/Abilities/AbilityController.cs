using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;

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

    public void UseAttackAbility(PlayerAbility ability)
    {
        
    }
}

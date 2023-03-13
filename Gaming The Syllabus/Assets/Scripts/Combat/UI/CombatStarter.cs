using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStarter : MonoBehaviour
{
    
    //[SerializeField] private GameObject combatUIGameObject;

    [Header("DO NOT INCLUDE 'ASSETS/' IN THE PATH")]
    [SerializeField] private List<string> enemyFilePaths;

    void OnCollisionEnter2D(Collision2D col)
    {
        StartCombat();
    }

    public void StartCombat()
    {
        DatabaseManager.GetPlayerXmlFromDB("Player.xml");
        List<CombatUnit> combatUnits = new(enemyFilePaths.Count);
        
        foreach(string enemyFilePath in enemyFilePaths)
        {
            DatabaseManager.GetEnemyXmlFromDB(enemyFilePath);
            combatUnits.Add(XmlUtilities.Deserialize<CombatUnit>("XML/" + enemyFilePath));
        }

        CombatManager.StartCombat(enemies: combatUnits.ToArray());
    }
}

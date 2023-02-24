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
        List<CombatUnit> combatUnits = new(enemyFilePaths.Count);
        enemyFilePaths.ForEach((enemyFilePath) => combatUnits.Add(XmlUtilities.Deserialize<CombatUnit>(enemyFilePath)));

        CombatManager.StartCombat(enemies: combatUnits.ToArray());
    }
}

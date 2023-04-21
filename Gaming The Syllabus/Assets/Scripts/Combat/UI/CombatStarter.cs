using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStarter : MonoBehaviour
{
    [SerializeField] private List<string> enemyFileNames;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (DatabaseManager.EnemiesHaveBeenLoaded)
        {
            StartCombat();
        }
    }

    public void StartCombat()
    {
        List<CombatUnit> combatUnits = new(enemyFileNames.Count);
        
        foreach(string enemyFileName in enemyFileNames)
        {
            combatUnits.Add(XmlUtilities.Deserialize<CombatUnit>(System.IO.Path.Combine(Files.EnemiesFolder, enemyFileName)));
        }

        CombatManager.StartCombat(enemies: combatUnits.ToArray());
    }

    void Update()
    {
        if (CombatManager.player != null && CombatManager.CheckCombatIsOver())
        {
            gameObject.SetActive(false);
        }
    }
}

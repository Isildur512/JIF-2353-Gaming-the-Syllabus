using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class CombatStarter : MonoBehaviour
{
    [SerializeField] private string combatFileName;

    private List<string> enemyFileNames;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (DatabaseManager.ContentIsLoaded(DatabaseManager.Loadable.Enemies) 
         && DatabaseManager.ContentIsLoaded(DatabaseManager.Loadable.Player))
        {
            StartCombat();
        }
    }

    public void StartCombat()
    {
        enemyFileNames = new();

        List<CombatUnit> combatUnits = new();

        XmlDocument combatDataXml = new XmlDocument();
        StreamReader reader = new StreamReader(Path.Combine(Files.CombatsFolder, combatFileName));
        combatDataXml.Load(reader);

        XmlNodeList items = combatDataXml.GetElementsByTagName("Unit");
        for (int i = 0; i < items.Count; i++)
        {
            string enemyFileName = items[i].InnerXml;
            combatUnits.Add(XmlUtilities.Deserialize<CombatUnit>(Path.Combine(Files.EnemiesFolder, enemyFileName)));
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
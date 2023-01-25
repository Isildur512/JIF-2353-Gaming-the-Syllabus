using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// Singleton class for handling everything related to combat.
/// </summary>
public class NewCombatManager : Singleton<NewCombatManager>
{
    private CombatUnit player;
    private CombatUnit[] enemies;

    [SerializeField] private CombatUnit testEnemy;
    [SerializeField] private CombatUnit testPlayer; // This should probly not be a CombatUnit... make player a separate class (or maybe inherit from CombatUnit?)

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        player = new CombatUnit();
        player.currentHealth = 10;

        testEnemy = new CombatUnit();
        testEnemy.currentHealth = 5;
        testEnemy.maximumHealth = 10;
        testEnemy.unitName = "Me";

        List<UnitAction> unitActions = new List<UnitAction>();
        unitActions.Add(new UnitAction(new DamageTarget(5, TargetType.Player)));
        testEnemy.actions = unitActions;

        XmlUtilities.Serialize(testEnemy, "test.xml");
        CombatUnit testEnemy2 = XmlUtilities.Deserialize<CombatUnit>("test.xml");
        testEnemy2.PerformTurn();

        Debug.Log("Player HP after enemy turn: " + player.currentHealth);

        //StartCombat(testPlayer, testEnemy);
    }

    /// <summary>
    /// Returns the correct targets in the current combat based on the target type.
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public static CombatUnit[] GetTargetsByType(TargetType targetType)
    {
        switch (targetType)
        {
            case TargetType.Player:
                return new CombatUnit[1] { _instance.player };
            default:
                return null;
        }
    }

    public static void StartCombat(CombatUnit player, params CombatUnit[] enemies)
    {
        _instance.enemies = enemies;
        _instance.player = player;

        _instance.enemies[0].PerformTurn();
    }
}

public enum TargetType
{
    Current, // Use whatever target we already have
    Player,
    AnyEnemy,
    AllEnemies
}

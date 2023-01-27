using System.Diagnostics.Tracing;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// Singleton class for handling everything related to combat.
/// </summary>
public class CombatManager : Singleton<CombatManager>
{
    public PlayerUnit player;
    public CombatUnit[] enemies;
    
    public Queue<CombatUnit> turnQueue;

    public bool isInitialized {get; private set;} // Checks if CombatManager is loaded. I want CombatManager loaded in before AIController.

    [SerializeField] private CombatUnit testEnemy;
    [SerializeField] private CombatUnit testPlayer; // This should probly not be a CombatUnit... make player a separate class (or maybe inherit from CombatUnit?)

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Start()
    {
        // player = new CombatUnit();
        player = XmlUtilities.Deserialize<PlayerUnit>("Scripts/Player/Player.xml");
        testEnemy = XmlUtilities.Deserialize<CombatUnit>("Scripts/Enemy/Enemy.xml");
        enemies = new CombatUnit[1];
        enemies[0] = testEnemy;

        turnQueue = new Queue<CombatUnit>();
        turnQueue.Enqueue(player);

        foreach (CombatUnit enemy in enemies) {
            turnQueue.Enqueue(enemy);
        }

        isInitialized = true;

        // List<UnitAction> unitActions = new List<UnitAction>();
        // unitActions.Add(new UnitAction(new DamageTarget(5, TargetType.Player)));
        // testEnemy.actions = unitActions;

        // XmlUtilities.Serialize(testEnemy, "test.xml");
        // CombatUnit testEnemy2 = XmlUtilities.Deserialize<CombatUnit>("test.xml");
        // testEnemy2.PerformTurn();

        Debug.Log("Player HP after enemy turn: " + player.currentHealth);

        //StartCombat(testPlayer, testEnemy);
    }

    public void nextTurn() {
        turnQueue.Enqueue(turnQueue.Dequeue()); // Puts current turn to end of queue. Basically ends the current turn.
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
                return new PlayerUnit[1] { _instance.player };
            case TargetType.AnyEnemy:
                return new CombatUnit[1] { _instance.enemies[Random.Range(0, _instance.enemies.Length)] };
            default:
                return null;
        }
    }

    public static void StartCombat(PlayerUnit player, params CombatUnit[] enemies)
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

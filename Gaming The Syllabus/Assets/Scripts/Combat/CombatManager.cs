using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton class for handling everything related to combat.
/// </summary>
public enum GameState {
    Ongoing,
    Win,
    Lose
}

public class CombatManager : Singleton<CombatManager>
{
    public static GameState gameStatus { get; private set; }
    public static CombatUnit player { get; private set; }
    private static CombatUnit[] enemies;

    private static List<CombatUnit> allCombatants;

    public static CombatUnit currentCombatant { get; private set; }

    private static int indexOfCombatantWithCurrentTurn;

    public static bool isDone { get; private set; } // Checks if this script is done running.

    public static bool IsPlayerTurn { get; private set; }

    private void Awake()
    {
        isDone = false;
        InitializeSingleton();
    }

    /// <summary>
    /// Use this to run coroutines related to combat in classes that don't inherit from MonoBehaviour.
    /// </summary>
    /// <param name="methodName"></param>
    public static void StartCombatCoroutine(IEnumerator coroutine)
    {
        _instance.StartCoroutine(coroutine);
    }

    public static void PerformPlayerAction(PlayerAction playerAction)
    {
        if (IsPlayerTurn)
        {
            player.PerformAction((int)playerAction);
            IsPlayerTurn = false;
        }
    }


    public static void NextTurn() {

        if (CheckCombatIsOver()) {
            EndCombat();
            return;
        }
         // Move to next unit
        AdvanceCurrentTurnIndex();

        // Skip over dead combatants
        while (!allCombatants[indexOfCombatantWithCurrentTurn].IsAlive)
        {
            AdvanceCurrentTurnIndex();
        }

        CombatUnit unitWithCurrentTurn = allCombatants[indexOfCombatantWithCurrentTurn];
        CombatUIManager.MarkUnitAsTakingTurn(unitWithCurrentTurn);
        currentCombatant = allCombatants[indexOfCombatantWithCurrentTurn];

        if (unitWithCurrentTurn.Equals(player))
        {
            IsPlayerTurn = true;
        } 
        else
        {
            IsPlayerTurn = false;
            unitWithCurrentTurn.PerformRandomAction();
        }

    }


    private static void AdvanceCurrentTurnIndex()
    {
        indexOfCombatantWithCurrentTurn = (indexOfCombatantWithCurrentTurn + 1) % allCombatants.Count;
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
                return new CombatUnit[1] { player };
            case TargetType.AnyEnemy:
                return new CombatUnit[1] { GetRandomLivingEnemy() };
            case TargetType.AllEnemies:
                CombatUnit[] copy = new CombatUnit[enemies.Length];
                enemies.CopyTo(copy, 0);
                return copy;
            default:
                return null;
        }
    }

    public static CombatUnit GetRandomLivingEnemy()
    {
        List<CombatUnit> livingEnemies = allCombatants.FindAll((enemy) => enemy.IsAlive);
        if (!player.IsAlive) {
            return livingEnemies[0];
        }
        return livingEnemies[Random.Range(1, livingEnemies.Count)];

    }


    public static bool CheckCombatIsOver() {
        if (player == null || !player.IsAlive) {
            return true;
        }

        List<CombatUnit> combatants = allCombatants.FindAll((combatant) => combatant.IsAlive);

        if (combatants.Count == 1 && (combatants[0].IsAlive && combatants[0] == player)) { // Checks if all enemies are dead and only play left alive.
            return true;
        }

        return false;
    }

    public static void StartCombat(CombatUnit player = null, params CombatUnit[] enemies)
    {
        CombatManager.enemies = enemies;
        CombatManager.player = player;

        if (CombatManager.player == null)
        {
            CombatManager.player = XmlUtilities.Deserialize<CombatUnit>(Files.PlayerXml);
        }

        allCombatants = new List<CombatUnit>();
        allCombatants.Add(CombatManager.player);
        foreach (CombatUnit enemy in CombatManager.enemies)
        {
            allCombatants.Add(enemy);
        }

        CombatUIManager.CombatUIActive = true;
        CombatUIManager.InitializeHealthbars(CombatManager.player, CombatManager.enemies);

        AbilityController.GiveDefaultAbilities(CombatManager.player);
        AbilityPageUIManager.SetupAbilitiesUI();

        Player.CanMove = false;

        gameStatus = GameState.Ongoing;

        // NextTurn advances our actingCombatantIndex so we want to start at -1 so the player goes first
        indexOfCombatantWithCurrentTurn = -1;
        NextTurn();
        isDone = true;
    }

    public static void EndCombat()
    {
        Player.CanMove = true;
        CombatUIManager.DeactivateCombatUI();
        CombatUIManager.CombatUIActive = false;
        
        if (player.IsAlive)
        {
            gameStatus = GameState.Win;
        }
        else
        {
            gameStatus = GameState.Lose;
            LevelLoader._instance.LoadLevel(SceneManager.GetActiveScene());
        }
    }
}

public enum PlayerAction
{
    Attack,
    Defend,
    Rest
}

public enum TargetType
{ 
    None,
    // Use whatever target we already have.
    // This might be useful for player abilities with multiple effects where we want them to only select a target once.
    Current, 
    Player,
    AnyEnemy,
    AllEnemies
}

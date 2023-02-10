using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Currently handles displaying player and enemy healthbars in combat.
/// </summary>
public class CombatUIManager : Singleton<CombatUIManager>
{
    [SerializeField] private GameObject healthbarPrefab;

    [SerializeField] private Vector2 firstEnemyHealthbarPosition;
    [SerializeField] private float xDistanceBetweenEnemyHealthbars = 225;
    [SerializeField] private Vector2 playerHealthbarPosition;
    private Vector2 nextHealthbarPosition;

    private static Dictionary<CombatUnit, Healthbar> combatUnitToHealthbar;

    private void Awake()
    {
        InitializeSingleton();
        nextHealthbarPosition = firstEnemyHealthbarPosition;
    }

    public static void InitializeHealthbars(CombatUnit player, params CombatUnit[] allEnemies)
    {
        combatUnitToHealthbar = new();

        combatUnitToHealthbar.Add(player, CreateHealthbar(player, _instance.playerHealthbarPosition));

        foreach (CombatUnit unit in allEnemies)
        {
            combatUnitToHealthbar.Add(unit, CreateHealthbar(unit, _instance.nextHealthbarPosition));
            _instance.nextHealthbarPosition.x += _instance.xDistanceBetweenEnemyHealthbars;
        }
    }

    public static void UpdateUnitHealthbar(CombatUnit unit, float currentHealth, float maximumHealth = -1)
    {
        combatUnitToHealthbar[unit].UpdateHealthbarValue(currentHealth, maximumHealth);
    }

    public static void UpdateHealthbarText(CombatUnit unit, int amount) {
        combatUnitToHealthbar[unit].UpdateHealthChangeText(amount.ToString());
        combatUnitToHealthbar[unit].ShowHealthChangeText();
    }


    /// <summary>
    /// Visually indicates on the UI that this unit is currently taking their turn. 
    /// All other units will be updated to no longer be displayed as taking their turn.
    /// </summary>
    public static void MarkUnitAsTakingTurn(CombatUnit unit)
    {
        foreach (KeyValuePair<CombatUnit, Healthbar> keyValuePair in combatUnitToHealthbar)
        {
            (CombatUnit unit, Healthbar bar) pair = (keyValuePair.Key, keyValuePair.Value);
            pair.bar.MarkAsCurrentTurn(pair.unit.Equals(unit));
        }
    }

    private static Healthbar CreateHealthbar(CombatUnit unit, Vector2 position)
    {
        GameObject healthbarGameObject = Instantiate(_instance.healthbarPrefab, _instance.transform);
        healthbarGameObject.name = $"{unit.UnitName}Healthbar";

        RectTransform healthbarTransform = healthbarGameObject.GetComponent<RectTransform>();
        healthbarTransform.anchoredPosition = position;

        Healthbar healthbar = healthbarGameObject.GetComponent<Healthbar>();
        healthbar.UpdateUnitName(unit.UnitName);
        healthbar.UpdateHealthbarValue(unit.CurrentHealth, unit.MaximumHealth);


        return healthbarGameObject.GetComponent<Healthbar>();
    }

}
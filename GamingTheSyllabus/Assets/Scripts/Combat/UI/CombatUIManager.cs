using System.Runtime.Serialization;
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

    [SerializeField] private GameObject actionBox;
    [SerializeField] private GameObject abilityPage;

    [SerializeField] private GameObject combatUIObject;
    public static bool CombatUIActive { 
        get => _instance.combatUIObject.activeInHierarchy; 
        set
        {
            _instance.combatUIObject.SetActive(value);
        } 
    }

    [SerializeField] private Vector2 firstEnemyHealthbarPosition;
    [SerializeField] private float xDistanceBetweenEnemyHealthbars = 225;
    [SerializeField] private Vector2 playerHealthbarPosition;
    private Vector2 nextHealthbarPosition;

    private static Dictionary<CombatUnit, Healthbar> combatUnitToHealthbar = new();

    private void Awake()
    {
        InitializeSingleton();
        nextHealthbarPosition = firstEnemyHealthbarPosition;

        combatUIObject.SetActive(false);
    }

    public static void RemoveAllHealthbars()
    {
        foreach (Healthbar healthbar in combatUnitToHealthbar.Values)
        {
            Destroy(healthbar.gameObject);
        }

        _instance.nextHealthbarPosition = _instance.firstEnemyHealthbarPosition;
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

    public static void UpdateHealthbarText(CombatUnit unit, int healthAmount)
    {
        combatUnitToHealthbar[unit].UpdateHealthChangeText(healthAmount.ToString());
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
        GameObject healthbarGameObject = Instantiate(_instance.healthbarPrefab, _instance.combatUIObject.transform);
        healthbarGameObject.name = $"{unit.UnitName}Healthbar";

        RectTransform healthbarTransform = healthbarGameObject.GetComponent<RectTransform>();
        healthbarTransform.anchoredPosition = position;

        Healthbar healthbar = healthbarGameObject.GetComponent<Healthbar>();
        healthbar.UpdateUnitName(unit.UnitName);
        healthbar.UpdateHealthbarValue(unit.CurrentHealth, unit.MaximumHealth);
        healthbar.UpdateUnitSprite(unit.Sprite);

        return healthbarGameObject.GetComponent<Healthbar>();
    }

    public static void ToggleAbilityPageActive()
    {
        bool abilityPageIsActive = _instance.abilityPage.activeInHierarchy;
        _instance.abilityPage.SetActive(!abilityPageIsActive);
        _instance.actionBox.SetActive(abilityPageIsActive);
    }

    public static void DeactivateCombatUI() {
        _instance.combatUIObject.SetActive(false);
    }
}

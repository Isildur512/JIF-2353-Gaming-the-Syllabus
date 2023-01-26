using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatButtonActions : MonoBehaviour
{

    [SerializeField] NewCombatManager combatManager;

    public void attack() {
        CombatUnit[] x = NewCombatManager.GetTargetsByType(TargetType.AnyEnemy);
        combatManager.player.PerformTurn();
    }
}

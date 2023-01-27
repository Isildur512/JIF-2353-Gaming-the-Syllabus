using System.Security.Authentication;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatButtonActions : MonoBehaviour
{

    [SerializeField] CombatManager combatManager;

    public void attack() {
        combatManager.player.PerformTurn(PlayerActions.basicAttack);
        Debug.Log(combatManager.turnQueue.Count);
    }

    public void rest() {
        combatManager.player.PerformTurn(PlayerActions.rest);
    }
}

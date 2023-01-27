using System.Security.Authentication;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatButtonActions : MonoBehaviour
{

    [SerializeField] CombatManager combatManager;

    public void attack() {
        combatManager.player.PerformTurn(Player.Actions.basicAttack);
    }

    public void rest() {
        combatManager.player.PerformTurn(Player.Actions.rest);
    }
}

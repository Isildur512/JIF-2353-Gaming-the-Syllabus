using System.Security.Authentication;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActionButton : MonoBehaviour
{
    [SerializeField] private PlayerAction playerAction;

    public void OnClick()
    {
        // CombatManager.PerformPlayerAction(playerAction);
        PowerUp damagePowerUp = new DamagePowerUp();
        Debug.Log(((DamagePowerUp)damagePowerUp).damageIncreaseAmt);
        // CombatManager.PerformPlayerAction(playerAction);
    }
}

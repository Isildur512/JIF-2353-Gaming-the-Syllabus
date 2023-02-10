using System.Security.Authentication;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActionButton : MonoBehaviour
{
    [SerializeField] private PlayerAction playerAction;
    [SerializeField] private PlayerAbility playerAbility;

    public void AttackOnClick()
    {
        CombatManager.PerformPlayerAction(playerAction);
    }

    public void AbilityOnClick()
     {
        CombatManager.PerformPlayerAbility(playerAbility);
     }
}

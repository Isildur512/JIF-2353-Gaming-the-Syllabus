using System.Security.Authentication;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActionButton : MonoBehaviour
{
    [SerializeField] private PlayerAction playerAction;
    [SerializeField] private GameObject abilityPage;
    [SerializeField] private GameObject actionBox;

    public void OnClick()
    {
        CombatManager.PerformPlayerAction(playerAction);
    }

    public void ShowAbilityPage()
    {
        if (CombatManager.IsPlayerTurn)
        {
            CombatUIManager.ToggleAbilityPageActive();
        }
    }

}

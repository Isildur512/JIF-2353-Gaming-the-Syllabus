using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButton : MonoBehaviour
{
    public void OnClick()
    {
        CombatUIManager.ToggleAbilityPageActive();
    }
}

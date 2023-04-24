using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScreenManager : Singleton<InstructionsScreenManager>
{
    [SerializeField] private GameObject instructionsScreen;

    private void Awake()
    {
        InitializeSingleton();
    }

    public static void SetScreenActive(bool isActive)
    {
        if (!CombatUIManager.CombatUIActive && !SyllabusRiddleUIManager.UIIsActive)
        {
            _instance.instructionsScreen.SetActive(isActive);
            Player.CanMove = !isActive;
        }
    }
}

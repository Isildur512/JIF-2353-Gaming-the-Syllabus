using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugTextManager : Singleton<DebugTextManager>
{
    [SerializeField] private TextMeshProUGUI textElement;

    private void Awake()
    {
        InitializeSingleton();
    }

    public static void Log(string text)
    {
        _instance.textElement.text = text;
    }
}

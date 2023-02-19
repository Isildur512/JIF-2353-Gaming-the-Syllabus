using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TooltipManager : Singleton<TooltipManager>
{
    [SerializeField] private TextMeshProUGUI textComponent;

    void Awake()
    {
        InitializeSingleton();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }


    public static void SetAndShowTooltip(string message)
    {
        _instance.gameObject.SetActive(true);
        _instance.textComponent.text = message;
    }

    public static void HideToolTip()
    {
        _instance.gameObject.SetActive(false);
        _instance.textComponent.text = string.Empty;
    }
}

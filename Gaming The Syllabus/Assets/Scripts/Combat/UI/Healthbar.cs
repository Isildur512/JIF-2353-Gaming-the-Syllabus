using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private TextMeshProUGUI unitName;
    [SerializeField] private Image sprite;

    public void UpdateHealthbarValue(float currentValue, float maxValue = -1)
    {
        if (maxValue > 0)
        {
            bar.maxValue = maxValue;
        }
        bar.value = currentValue;
    }
    public void UpdateUnitName(string newText)
    {
        unitName.text = newText;
    }

    /// <summary>
    /// Visually indicates on the UI that this unit is or is not the one currently taking their turn.
    /// </summary>
    public void MarkAsCurrentTurn(bool isCurrentTurn)
    {
        sprite.color = isCurrentTurn ? Color.green : Color.white;
    }
}

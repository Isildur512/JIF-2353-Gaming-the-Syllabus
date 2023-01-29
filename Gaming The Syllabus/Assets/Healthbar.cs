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
    [SerializeField] private TextMeshProUGUI healthChangeText; // UI to pop up when combat unit health changes

    private float currHealthChangeTextAlpha;

    void Update() {
        healthChangeText.alpha = Mathf.MoveTowards(
                                    healthChangeText.alpha, 
                                    0f, 
                                    0.65f * Time.deltaTime);
        
    }

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

    public void UpdateHealthChangeText(string newText)
    {
        if (int.Parse(newText) < 0) {
            healthChangeText.color = Color.red;
            healthChangeText.text = newText;
        } else if (int.Parse(newText) > 0) {
            healthChangeText.text = "+" + newText;
            healthChangeText.color = Color.green;
        }
    }

    public void ShowHealthChangeText() {
        healthChangeText.alpha = 1f;
    }

    /// <summary>
    /// Visually indicates on the UI that this unit is or is not the one currently taking their turn.
    /// </summary>
    public void MarkAsCurrentTurn(bool isCurrentTurn)
    {
        sprite.color = isCurrentTurn ? Color.green : Color.white;
    }
}

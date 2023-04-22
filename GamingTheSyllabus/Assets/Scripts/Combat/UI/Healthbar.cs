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

    public void UpdateUnitSprite(Sprite sprite)
    {
        this.sprite.sprite = sprite;
    }


    /// <summary>
    /// Updates the text seen below a Combat Unit's health bar. For example, if a combat unit
    /// takes 5 damage then a 5 will popup on screen below their health.
    /// </summary>
    /// <param name="amtChanged">Make sure you pass in a string represented integer. For example, "5" or "-5"</param>
    public void UpdateHealthChangeText(string amtChanged)
    {
        if (int.Parse(amtChanged) < 0) {
            healthChangeText.color = Color.red;
            healthChangeText.text = amtChanged;
        } else if (int.Parse(amtChanged) > 0) {
            healthChangeText.text = "+" + amtChanged;
            healthChangeText.color = Color.green;
        }
        ShowHealthChangeText();
    }

    private void ShowHealthChangeText() {
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

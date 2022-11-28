using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatHUD : MonoBehaviour
{
    public Text nameText;
    public Slider hpSlider;

    public void setHUD(Unit unit) {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.curHP;
    }

    public void setHp(int hp) {
        hpSlider.value = hp;
    } 
}

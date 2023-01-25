using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerStats {
        public int health;
        public int damage;
    }

    [SerializeField]
    private PlayerStats stats;

    public int getHealth() {
        return stats.health;
    }

    public void setHealth(int amt) {
        stats.health = amt;
    }

    public int getDamage() {
        return stats.damage;
    }

    public void setDamage(int amt) {
        stats.damage = amt;
    }
}

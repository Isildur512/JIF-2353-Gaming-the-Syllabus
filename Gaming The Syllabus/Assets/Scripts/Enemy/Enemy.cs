using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyStats {
        public int health;
        public int damage;
    }

    [SerializeField]
    private EnemyStats stats;

    public int getHealth() {
        return stats.health;
    }

    public void setHealth(int amt) {
        stats.health = amt;
    }

    public int getDamage() {
        return stats.health;
    }

    public void setDamage(int amt) {
        stats.damage = amt;
    }
}

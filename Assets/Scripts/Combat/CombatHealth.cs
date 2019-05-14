using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHealth : MonoBehaviour
{
    public int health;
    public HealthUI healthUI;
    public EnemyHealthBar enemyBar;

    public bool Damage(int amount)
    {
        health -= amount;

        if (health <= 0) 
            return true;
        if(healthUI)
            healthUI.UpdateHealth(amount);
        if (enemyBar)
            enemyBar.UpdateHealth(amount);

        return false;
    }
}

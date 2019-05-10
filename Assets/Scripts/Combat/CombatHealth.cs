using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHealth : MonoBehaviour
{
    public int health;
    public healthUI healthUI;
    public EnemyHealthBar enemyBar;

    public bool Damage(int amount)
    {
        health -= amount;
        if(healthUI)
            healthUI.UpdateHealth(amount);
        if (enemyBar)
            enemyBar.UpdateHealth(amount);

        if (health <= 0) 
            return true;

        return false;
    }
}

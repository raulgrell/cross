using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHealth : MonoBehaviour
{
    public int health;
    public healthUI healthUI;

    public bool Damage(int amount)
    {
        health -= amount;
        healthUI.UpdateHealth(amount);

        return false;
    }
}

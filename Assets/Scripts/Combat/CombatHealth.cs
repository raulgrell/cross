using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHealth : MonoBehaviour
{
    public int health;

    public bool Damage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatThreat : MonoBehaviour
{
    public float lifetime = 0.5f;

    private void Update()
    {
        if (lifetime < 0)
            Destroy(gameObject);
        lifetime -= Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public List<Transform> healthHearts = new List<Transform>();

    public void UpdateHealth(int Damage)
    {
        for(int i = 0; i < Damage; i++)
        {
            healthHearts[i].gameObject.SetActive(false);
            healthHearts.RemoveAt(i);
        }
    }
}

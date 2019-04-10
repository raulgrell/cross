using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthUI : MonoBehaviour
{
    private List<Transform> healthHearts = new List<Transform>();

    private void Start()
    {
        for(int i = 0; i < healthHearts.Count; i++)
        {
            healthHearts[i] = transform.GetChild(i);
        }
    }

    public void UpdateHealth(int Damage)
    {
        for(int i = 0; i < Damage; i++)
        {
            healthHearts[i].gameObject.SetActive(false);
            healthHearts.RemoveAt(i);
        }
    }
}

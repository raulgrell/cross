using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public List<Transform> healthHearts = new List<Transform>();
    public  Quaternion fixedRotation;

    public void UpdateHealth(int Damage)
    {
        for (int i = 0; i < Damage; i++)
        {
            healthHearts[i].gameObject.SetActive(false);
            healthHearts.RemoveAt(i);
        }
        
        transform.rotation = fixedRotation;
    }

    public void FixRotation()
    {
        transform.rotation = fixedRotation;
    }


}

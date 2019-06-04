using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLauncher : MonoBehaviour
{
    public Transform start;
    public Transform end;

    public List<Transform> projectiles;

    private void Start()
    {
        projectiles = new List<Transform>();
        end = FindObjectOfType<PlayerController>().transform;
    }
    
    private void Update()
    {
        foreach (var p in projectiles)
        {
        }
    }
}

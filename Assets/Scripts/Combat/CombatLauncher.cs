using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLauncher : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float actionTime;
    [SerializeField] private Transform start;
    private Transform end;
    private GridCombat combat;
    private float timer;

    public GridCombat Combat => combat;
    
    private void Start()
    {
        combat = GetComponent<GridCombat>();
        end = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        if (timer < 0 && Vector3.Distance(start.position, end.position) < 10)
        {
            CombatProjectile.Spawn(prefab, start.position, end.position, this);
            timer = Random.Range(actionTime - 0.2f, actionTime + 0.2f);
        }

        timer -= Time.deltaTime;
    }
}

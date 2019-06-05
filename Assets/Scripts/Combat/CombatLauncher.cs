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

    private Vector3 lastTarget;

    public GridCombat Combat => combat;
    public Vector3 Last => lastTarget;

    private void Awake()
    {
        combat = GetComponent<GridCombat>();
    }

    private void Start()
    {
        end = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        combat.Unit.LookAt(combat.Grid.WorldToCell(end.position));

        if (timer < 0 && Vector3.Distance(start.position, end.position) < 10)
        {
            lastTarget = end.position;
            CombatProjectile.Spawn(prefab, start.position, end.position, this);
            timer = Random.Range(actionTime - 0.2f, actionTime + 0.2f);
        }

        timer -= Time.deltaTime;
    }
}

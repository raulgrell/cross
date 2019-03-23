using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public abstract class UnitController : MonoBehaviour
{
    public GridUnit unit;
    public Transform target;
    public float health;

    private void Start()
    {
        unit = GetComponent<GridUnit>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridUnit))]
public abstract class UnitController : MonoBehaviour
{
    public Transform target;
    public float health;
    
    internal GridUnit unit;

    private void Start()
    {
        unit = GetComponent<GridUnit>();
    }
}

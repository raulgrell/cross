using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour
{
    public GridUnit unit;
    public float height;
    public bool walkable;
    public Vector2Int gridPosition;

    public int gCost;
    public int hCost;
    public GridNode parent;

    public int FCost => gCost + hCost;
}

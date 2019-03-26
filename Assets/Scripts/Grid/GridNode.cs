using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour, IHeapItem<GridNode>
{
    public GridUnit unit;
    public float height;    
    public bool walkable = true;
    public Vector2Int gridPosition;

    public int gCost;
    public int hCost;
    public GridNode parent;

    public int FCost => gCost + hCost;
        
    public int HeapIndex { get; set; }
    
    public static GridNode Spawn(GameObject prefab, bool isWalkable, Vector3 worldPos, Vector2Int gridPos, Transform parent)
    {
        var obj = Instantiate(prefab, worldPos, Quaternion.identity, parent);
        var node = obj.GetComponent<GridNode>();
        obj.transform.position = worldPos;
        node.walkable = isWalkable;
        node.gridPosition = gridPos;
        return node;
    }
    
    public int CompareTo(GridNode gridNodeToCompare)
    {
        int compare = FCost.CompareTo(gridNodeToCompare.FCost);
        if (compare == 0)
            compare = hCost.CompareTo(gridNodeToCompare.hCost);
        return -compare;
    }
}

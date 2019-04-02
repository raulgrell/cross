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
    
    public static GridNode Spawn(GameObject prefab, Vector3 worldPos, Vector2Int gridPos, Transform parent)
    {
        var obj = Instantiate(prefab, worldPos, Quaternion.identity, parent);
        obj.transform.position = worldPos;
        
        var node = obj.GetComponent<GridNode>();
        node.walkable = true;
        node.gridPosition = gridPos;

        if (Physics.Raycast(worldPos + Vector3.up * 2, Vector3.down, out RaycastHit hitInfo))
        {
            node.height = hitInfo.point.y;
            obj.transform.position = obj.transform.position.SetY(node.height);
        }

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

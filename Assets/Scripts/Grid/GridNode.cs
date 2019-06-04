using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public enum NodeType
{
    Slow,
    Normal,
    Full
}

public class GridNode : MonoBehaviour, IHeapItem<GridNode>
{
    public GridUnit unit;
    public float height;    
    public bool walkable = true;
    public Vector2Int gridPosition;
    public NodeType type;

    public int gCost;
    public int hCost;
    public GridNode parent;

    public int FCost => gCost + hCost;
        
    public int HeapIndex { get; set; }
    
    public static GridNode Spawn(GridBlocks blocks, Vector3 worldPos, Vector2Int gridPos, Transform parent, LayerMask customMask)
    {
        var blockIndex = Random.Range(0, blocks.metals.Length);
        var perlinIndex = Mathf.FloorToInt(Mathf.PerlinNoise(gridPos.x*0.1f, gridPos.y*0.1f) * blocks.metals.Length);
        var randomRotation = Random.Range(0, 4);

        GameObject obj;
        GridNode node;
        if (Physics.Raycast(worldPos, Vector3.down, out RaycastHit hitInfo, customMask))
        {
            if (hitInfo.collider.CompareTag("Walkway"))
            {
                var walkwayIndex = Random.Range(0, blocks.walkways.Length);
                obj = Instantiate(blocks.walkways[walkwayIndex], worldPos, Quaternion.Euler(0, 90 * randomRotation, 0), parent);
                node = obj.GetComponent<GridNode>();
                node.gridPosition = gridPos;
                node.walkable = true;
                return node;
            } 

            if (hitInfo.collider.CompareTag("Exterior"))
            {
                var exteriorIndex = Random.Range(0, blocks.exterior.Length);
                obj = Instantiate(blocks.exterior[exteriorIndex], worldPos, Quaternion.Euler(0, 90 * randomRotation, 0), parent);
                node = obj.GetComponent<GridNode>();
                node.gridPosition = gridPos;
                node.walkable = true;
                return node;
            }
            
            if (hitInfo.collider.CompareTag("Wall"))
            {
                obj = Instantiate(blocks.metals[blockIndex], worldPos, Quaternion.Euler(0, 90 * randomRotation, 0), parent);
                node = obj.GetComponent<GridNode>();
                node.gridPosition = gridPos;
                node.walkable = false;
                return node;
            }
            
            if (hitInfo.collider.CompareTag("Hole"))
            {
                obj = Instantiate(blocks.empty, worldPos, Quaternion.Euler(0, 90 * randomRotation, 0), parent);
                node = obj.GetComponent<GridNode>();
                node.gridPosition = gridPos;
                node.walkable = true;
                return node;
            }
        }

        bool isEven = (worldPos.x + worldPos.y) % 2 == 0;
        obj = Instantiate(blocks.metals[isEven ? 0 : blockIndex], worldPos, Quaternion.Euler(0, 90 * randomRotation, 0), parent);
        node = obj.GetComponent<GridNode>();
        node.gridPosition = gridPos;
        node.walkable = true;
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

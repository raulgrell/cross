using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class GridLayer : MonoBehaviour
{
    [SerializeField] private LayerMask customMask;
    [SerializeField] private Transform floor;
    [SerializeField] private GridBlocks floorBlocks;
    [SerializeField] private int numCols = 12;
    [SerializeField] private int numRows = 8;
    [SerializeField] private float cellSize = 2;
    [SerializeField] private float gutterSize = 0.5f;
    
    private GridNode[,] nodes;
    
    public int Count => numCols * numRows;
    public GridNode[,] Nodes => nodes;

    void Awake()
    {
        if (nodes == null)
            GenerateFloor();
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        var gutterOffset = new Vector3(gutterSize, 0, gutterSize);
        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                var offset = Vector3.forward * cellSize * j + Vector3.right * cellSize * i + gutterOffset / 2;
                Gizmos.DrawWireSphere(offset - gutterOffset / 2 + Vector3.up, 0.05f);
            }
        }
    }
    
    public bool InBounds(int x, int y)
    {
        return (x >= 0 && x < numCols && y >= 0 && y < numRows);
    }
    
    public bool IsEmpty(int x, int y)
    {
        return Nodes[y, x].unit == null;
    }

    public bool IsWalkable(int x, int y)
    {
        return InBounds(x, y) && IsEmpty(x, y) && Nodes[y, x].Walkable;
    }

    public Vector3 CellToWorld(Vector2Int position)
    {
        var offset = Vector3.right * cellSize * position.x + Vector3.forward * cellSize * position.y;
        return transform.TransformPoint(offset);
    }

    public Vector2Int WorldToCell(Vector3 worldPosition)
    {
        float layerWidth = numCols * cellSize;
        float layerHeight = numRows * cellSize;
        float percentX = Mathf.Clamp01((worldPosition.x - transform.position.x) / layerWidth);
        float percentY = Mathf.Clamp01((worldPosition.z - transform.position.z) / layerHeight);
        int x = Mathf.RoundToInt((numCols) * percentX);
        int y = Mathf.RoundToInt((numRows) * percentY);
        return new Vector2Int(x, y);
    }
    
    public GridNode WorldToNode(Vector3 worldPosition)
    {
        var cell = WorldToCell(worldPosition);
        return Nodes[cell.y, cell.x];
    }

    public List<GridNode> GetNeighbours(GridNode gridNode, int depth = 1)
    {
        var neighbours = new List<GridNode>();

        for (int x = -depth; x <= depth; x++)
        {
            for (int y = -depth; y <= depth; y++)
            {
                if (x == 0 && y == 0) continue;
                int checkX = gridNode.gridPosition.x + x;
                int checkY = gridNode.gridPosition.y + y;

                if (0 <= checkX && checkX < numCols && 0 <= checkY && checkY < numRows)
                    neighbours.Add(Nodes[checkY, checkX]);
            }
        }

        return neighbours;
    }
    
    public GridNode ClosestWalkableNode(GridNode node)
    {
        int maxRadius = Mathf.Max(numCols, numRows) / 2;
        for (int i = 1; i < maxRadius; i++)
        {
            GridNode n = FindWalkableInRadius(node.gridPosition.x, node.gridPosition.y, i);
            if (n != null)
                return n;
        }
        return null;
    }

    GridNode FindWalkableInRadius(int x, int y, int radius)
    {
        for (int i = -radius; i <= radius; i++)
        {
            int c = i + x;
            int r = i + y;

            // top, right, bottom, left
            if (IsWalkable(c, y + radius)) return Nodes[y + radius, c];
            if (IsWalkable(x + radius, r)) return Nodes[r, x + radius];
            if (IsWalkable(c, y - radius)) return Nodes[y - radius, c];
            if (IsWalkable(x - radius, r)) return Nodes[r, x - radius];
        }

        return null;
    }

    public static int GetDistance(GridNode gridNodeA, GridNode gridNodeB)
    {
        int dstX = Mathf.Abs(gridNodeA.gridPosition.x - gridNodeB.gridPosition.x);
        int dstY = Mathf.Abs(gridNodeA.gridPosition.y - gridNodeB.gridPosition.y);
        return dstX + dstY;
    }

    private void ClearFloor()
    {
        if (!floor) return;
        var children = new List<GameObject>();
        foreach(Transform child in floor) children.Add(child.gameObject);
        foreach (GameObject child in children) DestroyImmediate(child);
        nodes = null;
    }
    
    [Button("Regenerate Floor")]
    private void GenerateFloor()
    {
        ClearFloor();
        
        if (!floor || floor.childCount != 0)
            return;
        
        nodes = new GridNode[numRows, numCols];
        
        var origin = transform.position;
        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                var offset = new Vector3(cellSize * i, Random.Range(0f, 0.2f), cellSize * j);
                var node = GridNode.Spawn(floorBlocks, origin + offset, new Vector2Int(i, j), floor, customMask);
                nodes[j, i] = node;
            }
        }
    }
}
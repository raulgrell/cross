﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class GridLayer : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public GameObject nodePrefab;
    public int numCols = 12;
    public int numRows = 8;
    public float cellSize = 2;
    public float gutterSize = 0.5f;
    public GridNode[,] nodes;

    public int Count => numCols * numRows;

    void Awake()
    {
        nodes = new GridNode[numRows, numCols];

        var origin = transform.position;
        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                var offset = Vector3.forward * cellSize * j + Vector3.right * cellSize * i;
                var node = GridNode.Spawn(nodePrefab, origin + offset, new Vector2Int(i, j), transform);
                nodes[j, i] = node;
            }
        }
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
                Gizmos.DrawWireSphere(offset - gutterOffset / 2 + Vector3.up, 0.1f);
            }
        }
    }
    
    public bool InBounds(int x, int y)
    {
        return (x >= 0 && x < numCols && y >= 0 && y < numRows);
    }

    public bool IsWalkable(int x, int y)
    {
        return InBounds(x, y) && nodes[y, x].walkable;
    }
    
    public bool IsEmpty(int x, int y)
    {
        return nodes[y, x] == null;
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
        int x = Mathf.RoundToInt((numCols - 1) * percentX);
        int y = Mathf.RoundToInt((numRows - 1) * percentY);
        return new Vector2Int(x, y);
    }
    
    public GridNode WorldToNode(Vector3 worldPosition)
    {
        var cell = WorldToCell(worldPosition);
        return nodes[cell.y, cell.x];
    }

    public bool CellIsWalkable(Vector2Int position)
    {
        var gridNode = nodes[position.y, position.x];
        return IsWalkable(position.x, position.y) && gridNode.unit == null;
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
                    neighbours.Add(nodes[checkY, checkX]);
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
            int vx = i + x;
            int hy = i + y;

            // top, right, bottom, left
            if (IsWalkable(vx, y + radius))
                return nodes[y + radius, vx];
            if (IsWalkable(x + radius, hy))
                return nodes[hy, x + radius];
            if (IsWalkable(vx, y - radius))
                return nodes[y - radius, vx];
            if (IsWalkable(x - radius, hy))
                return nodes[hy, x - radius];
        }

        return null;
    }

    public static int GetDistance(GridNode gridNodeA, GridNode gridNodeB)
    {
        int dstX = Mathf.Abs(gridNodeA.gridPosition.x - gridNodeB.gridPosition.x);
        int dstY = Mathf.Abs(gridNodeA.gridPosition.y - gridNodeB.gridPosition.y);
        return dstX + dstY;
    }
}
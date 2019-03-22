using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class GridLayer : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public GameObject nodePrefab;
    public int numCols = 8;
    public int numRows = 8;
    public float cellSize = 2;
    public float gutterSize = 0.5f;

    public GridNode[,] nodes;

    public int Count => numCols * numRows;
    
    bool InBounds(int x, int y)
    {
        return (x >= 0 && x < numCols && y >= 0 && y < numRows);
    }

    bool IsWalkable(int x, int y)
    {
        return InBounds(x, y) && nodes[x, y].walkable;
    }

    void Awake()
    {
        nodes = new GridNode[numRows, numCols];

        var origin = transform.position;
        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                var offset = Vector3.forward * cellSize * j + Vector3.right * cellSize * i;
                
                var nodeObject = Instantiate(nodePrefab, origin + offset, Quaternion.identity, transform);
                nodes[j, i] = nodeObject.GetComponent<GridNode>();
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
                var offset = Vector3.forward * cellSize * j + Vector3.right * cellSize * i + gutterOffset;
                Gizmos.DrawWireCube(offset, new Vector3(2, 0.1f, 2) - gutterOffset);
            }
        }
    }

    public Vector3 CellToWorld(Vector2Int position)
    {
        var offset = Vector3.right * cellSize * position.x + Vector3.forward * cellSize * position.y;
        return transform.TransformPoint(offset);
    }

    public Vector2Int WorldToCell(Vector3 worldPosition)
    {
        var layerWidth = numCols * cellSize;
        var layerHeight = numRows * cellSize;
        float percentX = Mathf.Clamp01((worldPosition.x - layerWidth) / layerWidth);
        float percentY = Mathf.Clamp01((worldPosition.y - layerHeight) / layerHeight);
        int x = Mathf.RoundToInt((numCols - 1) * percentX);
        int y = Mathf.RoundToInt((numRows - 1) * percentY);
        return new Vector2Int(x, y);
    }

    public bool CellIsWalkable(Vector2Int position)
    {
        var gridNode = nodes[position.y, position.x];
        return IsWalkable(position.x, position.y) && gridNode.unit == null;
    }
    
    public List<GridNode> GetNeighbours(Node node, int depth = 1)
    {
        var neighbours = new List<GridNode>();

        for (int x = -depth; x <= depth; x++)
        {
            for (int y = -depth; y <= depth; y++)
            {
                if (x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < numCols && checkY >= 0 && checkY < numRows)
                    neighbours.Add(nodes[checkX, checkY]);
            }
        }

        return neighbours;
    }
}
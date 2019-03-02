using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.WSA;

public class GridLayer : MonoBehaviour
{
    public GameObject nodePrefab;
    public int numCols = 8;
    public int numRows = 8;
    public float colWidth = 2;
    public float colHeight = 2;
    public float gutterSize = 0.1f;

    public GridNode[,] nodes;
    
    void Awake()
    {
        nodes = new GridNode[numRows, numCols];
        var origin = transform.position;
        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                var offset = Vector3.forward * colHeight * j + Vector3.right * colWidth * i;
                var nodeObject = Instantiate(nodePrefab, origin + offset, Quaternion.identity, transform);
                nodes[j, i] = nodeObject.GetComponent<GridNode>();
            }
        }
    }
    
    void Update()
    {

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        var gutterOffset = new Vector3(gutterSize, 0, gutterSize);
        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                var offset = Vector3.forward * colHeight * j + Vector3.right * colWidth * i + gutterOffset;
                Gizmos.DrawWireCube(offset, new Vector3(2, 0.1f, 2) - gutterOffset);
            }
        }
    }

    public Vector3 CellToWorld(Vector2Int position)
    {
        var offset = Vector3.right * colWidth * position.x + Vector3.forward * colHeight * position.y;
        return transform.TransformPoint(offset);
    }

    public bool CellIsWalkable(Vector2Int position)
    {
        return nodes[position.y, position.x].occupant == null;
    }
}

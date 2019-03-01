using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayer : MonoBehaviour
{
    public GameObject nodePrefab;
    public int numCols = 8;
    public int numRows = 8;
    public float colWidth = 2;
    public float colHeight = 2;
    public float gutterSize = 0.1f;
    
    void Start()
    {
        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                var offset = Vector3.forward * colHeight * j + Vector3.right * colWidth * i;
                Instantiate(nodePrefab, transform.position + offset, Quaternion.identity, transform);
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
                var offset = Vector3.forward * colHeight * j + Vector3.right * colWidth * i + gutterOffset;
                Gizmos.DrawWireCube(offset, new Vector3(2, 0.1f, 2) - gutterOffset);
            }
        }
    }

    void Update()
    {
        
    }

    public Vector3 CellToWorld(Vector2Int position)
    {
        var offset = Vector3.forward * colHeight * position.x + Vector3.right * colWidth * position.y;
        return transform.TransformPoint(offset);
    }
}

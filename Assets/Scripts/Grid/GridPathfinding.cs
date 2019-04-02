using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using Debug = System.Diagnostics.Debug;

public class GridPathfinding : MonoBehaviour
{
    public GridLayer grid;
    static GridPathfinding instance;

    void Awake()
    {
        instance = this;
    }

    public static GridNode[] RequestPath(Vector3 from, Vector3 to)
    {
        return instance.FindPath(from, to);
    }

    GridNode[] FindPath(Vector3 from, Vector3 to)
    {
        GridNode[] waypoints = null;
        var pathSuccess = false;
        
        var startNode = grid.WorldToNode(from);
        var targetNode = grid.WorldToNode(to);
                
        startNode.parent = startNode;

        if (!startNode.walkable)
            startNode = grid.ClosestWalkableNode(startNode);

        if (!targetNode.walkable)
            targetNode = grid.ClosestWalkableNode(targetNode);
        
        if (startNode.walkable && targetNode.walkable)
        {
            var openSet = new Heap<GridNode>(grid.Count);
            var closedSet = new HashSet<GridNode>();
            
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                GridNode currentGridNode = openSet.RemoveFirst();
                closedSet.Add(currentGridNode);
                
                if (currentGridNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (GridNode neighbour in grid.GetNeighbours(currentGridNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                        continue;

                    int newCostToNeighbour = currentGridNode.gCost + GridLayer.GetDistance(currentGridNode, neighbour);
                    if (newCostToNeighbour >= neighbour.gCost && openSet.Contains(neighbour))
                        continue;
                    
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GridLayer.GetDistance(neighbour, targetNode);
                    neighbour.parent = currentGridNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                    else
                        openSet.UpdateItem(neighbour);
                }
            }
        }

        if (pathSuccess)
        {
            waypoints = BuildPath(startNode, targetNode);
        }

        return waypoints;
    }

    GridNode[] BuildPath(GridNode startGridNode, GridNode endGridNode)
    {
        List<GridNode> path = new List<GridNode>();
        GridNode currentGridNode = endGridNode;

        while (currentGridNode != startGridNode)
        {
            path.Add(currentGridNode);
            currentGridNode = currentGridNode.parent;
        }

        GridNode[] waypoints = path.ToArray();
        Array.Reverse(waypoints);

        return waypoints;
    }
}

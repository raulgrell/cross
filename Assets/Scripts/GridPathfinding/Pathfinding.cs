using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using AI;

public class Pathfinding : MonoBehaviour
{
    public Grid grid;
    static Pathfinding instance;

    void Awake()
    {
        Init();
        instance = this;
    }

    public void Init()
    {
        grid = GetComponent<Grid>();
        grid.Init();
    }

    public static Vector2[] RequestPath(Vector2 from, Vector2 to)
    {
        return instance.FindPath(from, to);
    }

    Vector2[] FindPath(Vector2 from, Vector2 to)
    {
        var sw = new Stopwatch();
        sw.Start();

        var waypoints = new Vector2[0];
        var pathSuccess = false;
        
        var startNode = grid.NodeFromWorldPoint(from);
        var targetNode = grid.NodeFromWorldPoint(to);
        
        startNode.parent = startNode;

        if (!startNode.walkable)
            startNode = grid.ClosestWalkableNode(startNode);

        if (!targetNode.walkable)
            targetNode = grid.ClosestWalkableNode(targetNode);

        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.Count);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    sw.Stop();
                    print("Path found: " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                        continue;

                    int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    
                    if (newCostToNeighbour >= neighbour.gCost && openSet.Contains(neighbour))
                        continue;
                    
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                    else
                        openSet.UpdateItem(neighbour);
                }
            }
        }

        if (pathSuccess)
            waypoints = RetracePath(startNode, targetNode);

        return waypoints;
    }

    Vector2[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        Vector2[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;
    }

    Vector2[] SimplifyPath(List<Node> path)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
                waypoints.Add(path[i].worldPosition);
            directionOld = directionNew;
        }

        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);

        return 14 * dstX + 10 * (dstY - dstX);
    }
}

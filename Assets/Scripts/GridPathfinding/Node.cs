using UnityEngine;
using System.Collections;

namespace AI
{
    public class Node : IHeapItem<Node>
    {
        public bool walkable;
        public Vector2 worldPosition;
        public int gridX;
        public int gridY;
    
        public int gCost;
        public int hCost;
        public Node parent;
    
        public int HeapIndex { get; set; }
    
        public int FCost => gCost + hCost;
    
        public Node(bool walkable, Vector2 worldPos, int gridX, int gridY)
        {
            this.walkable = walkable;
            this.worldPosition = worldPos;
            this.gridX = gridX;
            this.gridY = gridY;
        }
    
        public int CompareTo(Node nodeToCompare)
        {
            int compare = FCost.CompareTo(nodeToCompare.FCost);
            if (compare == 0)
                compare = hCost.CompareTo(nodeToCompare.hCost);
            return -compare;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(GridUnit))]
    public abstract class UnitController : MonoBehaviour
    {
        public Transform target;
        public float health;
    
        internal GridUnit unit;
        internal GridNode[] path;
        internal int nodeIndex;
        internal Vector2Int gridWaypoint;
    
        private void Start()
        {
            unit = GetComponent<GridUnit>();
        }

        public void GoToNextWaypoint()
        {
            if (path == null || path.Length == 0 || nodeIndex >= path.Length)
                return;

            gridWaypoint = path[nodeIndex].gridPosition;
            nodeIndex += 1;
        }

        public bool IsAtWaypoint()
        {
            if (path == null || path.Length == 0 || nodeIndex >= path.Length)
                return false;
        
            return Vector3.Distance(transform.position, path[nodeIndex].transform.position) < 0.1f;
        }
    }    
}


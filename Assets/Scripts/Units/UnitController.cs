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
        internal GridCombat combat;
        internal GridNode[] path;
        internal int nodeIndex;
        internal Vector2Int gridWaypoint;
    
        private void Start()
        {
            unit = GetComponent<GridUnit>();
            combat = GetComponent<GridCombat>();
        }

        public void GoToNextWaypoint()
        {
            if (path == null || path.Length == 0 || nodeIndex >= path.Length)
                return;
            
            gridWaypoint = path[nodeIndex].gridPosition;
            unit.input = gridWaypoint - unit.position;
            nodeIndex += 1;
        }

        public bool IsAtWaypoint()
        {
            if (unit.state == GridUnitState.Moving)
                return false;
            
            if (path == null || nodeIndex >= path.Length)
                return false;

            return true;
        }

        public void Attack()
        {
            combat.meleeAttack.doAttack(Vector3.Angle(Vector3.forward, transform.forward), 3, AttackType.Melee);
        }
    }    
}


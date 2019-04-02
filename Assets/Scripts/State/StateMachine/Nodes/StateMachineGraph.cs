using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace QAI.FSM
{
    [CreateAssetMenu(fileName = "StateMachineGraph", menuName = "FSM/StateMachineGraph", order = 1)]
    public class StateMachineGraph : AIGraph
    {
        [SerializeField] private StateNode initialState;

        public void SetEntryState(StateNode state)
        {
            if (initialState != null)
                initialState.IsEntry = false;

            initialState = state;
        }

        public void UnsetEntryState(StateNode state)
        {
            if (state == initialState)
                initialState = null;
        }

        public override void Init(Blackboard blackboard)
        {
            _blackboard = blackboard;
            _blackboard.states[GetInstanceID()] = initialState;
            initialState.Enter();
        }

        public override Result Run(Blackboard blackboard)
        {
            _blackboard = blackboard;
            if (!_blackboard.states.TryGetValue(GetInstanceID(), out object state))
                return null;

            StateNode currentState = state as StateNode;
            if (currentState != null)
            {
                currentState.Run();
                StateNode newState = currentState.GetTransition();
                if (newState != null)
                {
                    currentState.Exit();
                    _blackboard.states[GetInstanceID()] = newState;
                    newState.Enter();
                }
            }

            _blackboard = null;
            return null;
        }
    }
}
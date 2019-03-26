using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace QAI.FSM
{
    [CreateAssetMenu(fileName = "StateMachineGraph", menuName = "FSM/StateMachineGraph", order = 1)]
    public class StateMachineGraph : AIGraph
    {
        [SerializeField] private StateNode _entryState;

        public void SetEntryState(StateNode state)
        {
            if (_entryState != null)
                _entryState.IsEntry = false;

            _entryState = state;
        }

        public void UnsetEntryState(StateNode state)
        {
            if (state == _entryState)
                _entryState = null;
        }

        public override void Init(Blackboard blackboard)
        {
            _blackboard = blackboard;
            _blackboard.states[GetInstanceID()] = _entryState;
            _entryState.Enter();
        }

        public override AIGraphRunner.Result Run(Blackboard blackboard)
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
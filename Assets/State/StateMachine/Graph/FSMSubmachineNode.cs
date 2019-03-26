using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QAI.FSM
{
    /// <summary>Finite State Machine node that will execute a sub machine.</summary>
    [CreateNodeMenu("FSM/State/Submachine")]
    public class FSMSubmachineNode : StateNode
    {
        /// <summary>A reference to the sub machine to be executed.</summary>
        [SerializeField] StateMachineGraph _machine;

        /// <summary>Execute state running submachine.</summary>
        public override void Run()
        {
            _machine.Run((graph as StateMachineGraph).Blackboard);
        }

        /// <summary>Executed on entering the state.</summary>
        public override void Enter()
        {
            // Check if machine has been initialized.
            Blackboard blackboard = (graph as StateMachineGraph).Blackboard;
            blackboard.variables.TryGetValue("Init" + GetInstanceID(), out object initialized);
            // If machine hasn't been initialized it is time to do so.
            if (initialized != null)
                return;
            
            _machine.Init((graph as StateMachineGraph).Blackboard);
            blackboard.variables["Init" + GetInstanceID()] = true;
        }

        public override void Exit()
        {
        }
    }
}
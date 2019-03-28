using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QAI.FSM
{
    [CreateNodeMenu("FSM/State/Submachine")]
    public class FSMSubmachineNode : StateNode
    {
        [SerializeField] StateMachineGraph submachine;

        public override void Run()
        {
            submachine.Run((graph as StateMachineGraph).Blackboard);
        }

        public override void Enter()
        {
            Blackboard blackboard = (graph as StateMachineGraph).Blackboard;
            if (blackboard.inits.Contains(GetInstanceID()))
                return;
            
            submachine.Init((graph as StateMachineGraph).Blackboard);
            blackboard.inits.Add(GetInstanceID());
        }

        public override void Exit()
        {
        }
    }
}
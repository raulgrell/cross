using System.Collections;
using System.Collections.Generic;
using QAI.BT;
using UnityEngine;
using XNode;

namespace QAI.FSM
{
    [Node.CreateNodeMenuAttribute("FSM/State/BehaviorTree")]
    public class FSMBehaviorTreeNode : StateNode
    {
        [SerializeField] BTGraph _tree;

        public override void Run()
        {
            var fsmGraph = graph as StateMachineGraph;
            _tree.Run(fsmGraph.Blackboard);
        }

        public override void Enter()
        {
            var fsmGraph = graph as StateMachineGraph;
            Blackboard blackboard = fsmGraph.Blackboard;
            if (blackboard.inits.Contains(GetInstanceID())) 
                return;
            
            _tree.Init(fsmGraph.Blackboard);
            blackboard.inits.Add(GetInstanceID());
        }

        public override void Exit()
        {
        }
    }
}
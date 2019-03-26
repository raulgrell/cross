using System;
using UnityEngine;

namespace QAI.FSM.Custom
{
    [CreateNodeMenu("FSM/Action/Shared")]
    class GenericAction : ActionNode
    {
        [SerializeField] private StateAction action;
        [SerializeField] private IntReference counter;

        public override void Run()
        {
            counter.Value += 1;
        }
    }
}
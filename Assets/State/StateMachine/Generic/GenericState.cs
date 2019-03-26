using System;
using UnityEngine;

namespace QAI.FSM.Custom
{
    [CreateNodeMenu("FSM/State/Shared")]
    class GenericState : StateNode
    {
        [SerializeField] private StateReference state;
        [SerializeField] private IntReference counter;

        public override void Run()
        {
            counter.Value += 1;
        }
    }
}
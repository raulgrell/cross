using System;
using UnityEngine;

namespace QAI.FSM.Custom
{
    [CreateNodeMenu("FSM/Action/Shared")]
    class GenericAction : ActionNode
    {
        [SerializeField] private UnitAction action;
        [SerializeField] private IntReference counter;
    }
}
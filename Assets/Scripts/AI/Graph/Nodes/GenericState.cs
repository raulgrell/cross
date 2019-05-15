using System;
using UnityEngine;

[CreateNodeMenu("FSM/State/Shared")]
class GenericState : StateNode
{
    [SerializeField] private StateVariable state;
    [SerializeField] private IntReference counter;

    public override void Run()
    {
        counter.Value += 1;
    }
}

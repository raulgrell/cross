using System;
using UnityEngine;

[CreateNodeMenu("FSM/Action/Shared")]
class GenericAction : ActionNode
{
    [SerializeField] private IntReference counter;
}

using System.Collections;
using System.Collections.Generic;
using QAI.FSM;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyBehaviour : StateMachine<EnemyController>
{
    public StateMachineGraph graph;
}

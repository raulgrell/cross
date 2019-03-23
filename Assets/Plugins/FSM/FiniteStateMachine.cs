using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiniteStateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
    public State initialState;

    private State currentState;

    private T agent;
    public T Agent => agent;

    private void Start()
    {
        currentState = initialState;
        agent = GetComponent<T>();
    }

    private void Update()
    {
        StateTransition triggered = null;

        foreach (var transition in currentState.Transitions)
        {
            if (transition.IsTriggered(this))
            {
                triggered = transition;
                break;
            }
        }

        var actions = new List<StateAction>();
        actions.AddRange(currentState.Actions);

        if (triggered)
        {
            if (currentState.ExitAction)
                actions.Add(currentState.ExitAction);

            if (triggered.Target.EntryAction)
                actions.Add(triggered.Target.EntryAction);

            if (triggered.Action)
                actions.Add(triggered.Action);

            DoActions(actions);
            currentState = triggered.Target;
        }
        else
        {
            DoActions(actions);
        }
    }

    private void DoActions(List<StateAction> actions)
    {
        foreach (var action in actions)
        {
            action.Act(this);
        }
    }
}
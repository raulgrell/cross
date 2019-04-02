using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
    public StateVariable initialState;
    private StateVariable currentState;

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

        foreach (var transition in currentState.Value.Transitions)
        {
            if (!transition.IsTriggered(this)) continue;
            triggered = transition;
            break;
        }

        var actions = new List<StateAction>();
        actions.AddRange(currentState.Value.Actions);

        if (triggered != null)
        {
            if (currentState.Value.ExitAction)
                actions.Add(currentState.Value.ExitAction);

            if (triggered.Target.Value.EntryAction)
                actions.Add(triggered.Target.Value.EntryAction);

            if (triggered.Action)
            {
                actions.Add(triggered.Action);
            }

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
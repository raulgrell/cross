using System;
using UnityEngine;

[Serializable]
public class State
{
    [SerializeField] private StateAction entryAction;
    [SerializeField] private StateAction exitAction;
    [SerializeField] private StateAction[] actions;
    [SerializeField] private StateTransition[] transitions;

    public StateAction EntryAction => entryAction;
    public StateAction ExitAction => exitAction;
    public StateAction[] Actions => actions;
    public StateTransition[] Transitions => transitions;
}
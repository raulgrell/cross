using System;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Blackboard : ScriptableObject
{
    public class StringDictionary : SerializableDictionary<string, Variable> {}

    [SerializeField] public StringDictionary variables = new StringDictionary();
}

public abstract class AIGraph : NodeGraph
{
    protected Blackboard blackboard;
    public Blackboard Blackboard => blackboard;

    public T GetValue<T>(string key)
    {
        if (blackboard == null)
            return default;

        if (blackboard.variables.TryGetValue(key, out Variable value))
            return value.GetType().IsAssignableFrom(typeof(T)) ? (T)value.value : default;

        return default;
    }

    public abstract void Init(Blackboard blackboard);

    public abstract bool Run(Blackboard blackboard);
}

public abstract class StateGraph : AIGraph
{
    public StateNode current;
}

[NodeWidth(360)]
public abstract class StateGraphNode : Node
{
    protected StateGraph Graph => graph as StateGraph;
}

[CreateAssetMenu(fileName = "StateGraph", menuName = "AI/StateGraph", order = 1)]
public class UnitGraph : StateGraph
{
    public override void Init(Blackboard b)
    {
    }

    public override bool Run(Blackboard b)
    {
        return false;
    }
}


[CreateNodeMenu("AI/State/Static")]
public class StateNode : StateGraphNode
{
    [SerializeField] private StateAction entryAction;
    [SerializeField] private StateAction exitAction;
    [SerializeField] private StateAction[] actions;

    [SerializeField]
    private List<StateGraphTransitionNode> transitions;

    public StateAction EntryAction => entryAction;
    public StateAction ExitAction => exitAction;
    public StateAction[] Actions => actions;
    public StateGraphTransitionNode[] Transitions => transitions.ToArray();

    public virtual void Run()
    {
    }
    
    public void Trigger()
    {
        (graph as StateGraph).current = this;
    }
}

[CreateNodeMenu("AI/Transition/Static")]
public class StateGraphTransitionNode : StateGraphNode
{
    [SerializeField] private StateCondition decision;
    [SerializeField] private StateAction action;
    [SerializeField] private StateNode target;

    public StateNode Target => target;
    public StateAction Action => action;

    public bool IsTriggered<T>(StateMachine<T> fsm) where T : MonoBehaviour
    {
        return decision.Test(fsm);
    }
}

[CreateNodeMenu("AI/Action/Static")]
public class ActionNode : StateGraphNode
{
    [SerializeField] private StateAction action;
}

[CreateNodeMenu("AI/State/SubGraph")]
public class StateSubGraph : StateGraphNode
{
    [Input] public bool exec;
    public NodeGraph subGraph;
    [Output] public bool output;
}

public abstract class VariableCondition : StateCondition
{
    public object _variable;
}

public class IsNullCheck : VariableCondition {

    public override bool Test<T>(StateMachine<T> fsm){
        // Check if variable is null.
        object variable = fsm.graph.GetValue<object>("_variable");
        bool isNull = variable == null;
        if (isNull) return true;
        
        // Check if variable is of value type.
        Type type = variable.GetType();
        if (type.IsValueType)
            isNull = variable == Activator.CreateInstance(type);
        
        return isNull;
    }
}
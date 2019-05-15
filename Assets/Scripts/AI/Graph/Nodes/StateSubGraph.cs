using XNode;

[CreateNodeMenu("AI/State/SubGraph")]
public class StateSubGraph : StateGraphNode
{
    public AIGraph subGraph;
    [Input] public bool input;
    [Output] public bool output;
}
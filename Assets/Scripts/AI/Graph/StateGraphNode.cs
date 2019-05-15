using XNode;

[NodeWidth(360)]
public abstract class StateGraphNode : Node
{
    protected StateGraph Graph => graph as StateGraph;
}
using XNode;

public abstract class AIGraph : NodeGraph
{
    public Blackboard blackboard;
    public World world;
    public Blackboard Blackboard => blackboard;

    public abstract void Init(Blackboard blackboard);
    public abstract bool Run(Blackboard blackboard);
}
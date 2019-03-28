using XNode;

namespace QAI
{
    [NodeTint("#FFBABB")]
    public abstract class VariableNode : Node
    {
        public abstract object GetValue();
    }
    
    public abstract class VariableNode<T> : VariableNode
    {
        [Output(ShowBackingValue.Always)] public T _value;
 
        public override object GetValue(NodePort port)
        {
            // Only respond to value requests.
            if (port.fieldName != "_value")
                return null;
 
            T value = (graph as AIGraph).GetValue<T>(name);
            return value;
        }
 
        public override object GetValue()
        {
            NodePort port = GetOutputPort("_value");
            return GetValue(port);
        }
    }
}
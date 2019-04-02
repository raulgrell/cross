using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace QAI.FSM
{
    public abstract class StateMachineNode : Node
    {
        protected StateMachineGraph FSM => graph as StateMachineGraph;
        
        public override object GetValue(NodePort port) => null;

        public T GetBlackboardValue<T>(string portName, VariableNode node, T fallback = default)
        {
            // Check if we need to change the fallback from the value in the node.
            T value = node != null ? (T) node.GetValue() : fallback;
            // Check connection override.
            value = GetInputValue<T>(portName, value);
            return value;
        }

        public T GetInputValue<T>(string portName)
        {
            NodePort port = GetInputPort(portName);
            return (T) GetValue(port);
        }

        /// <summary>Add component to the current game object.</summary>
        /// <remark>Triggers a warning to remember designer to add them manually.</remark>
        protected T AddComponent<T>() where T : MonoBehaviour
        {
            // Get game object.
            GameObject go = FSM.GetValue<GameObject>("GameObject");
            if (go == null)
            {
                Debug.LogError("No game object running this finite state machine.");
                return null;
            }

            // Add component and save it in blackboard.
            T component = go.AddComponent<T>();
            Debug.LogWarning($"Adding component {component} on execution.");
            return component;
        }

        protected T GetComponent<T>(string key) where T : MonoBehaviour
        {
            return FSM.GetValue<T>(key) ?? AddComponent<T>();
        }
    }
}
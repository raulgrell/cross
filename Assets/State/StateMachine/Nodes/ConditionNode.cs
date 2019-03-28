using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace QAI.FSM
{
    [NodeTintAttribute("#BAFFBE")]
    public abstract class ConditionNode : StateMachineNode
    {
        /// <summary>Connection to current state.</summary>
        [Input] public FSMConnection _entry;

        /// <summary>Variable to be used in the check.</summary>
        [Input] public VariableNode _variable = null;

        /// <summary>Connection to new state.</summary>
        [Output] public FSMConnection _exit = null;

        /// <summary>Whether to negate the result.</summary>
        [SerializeField] protected bool _negate = false;

        /// <summary>Whether the node has an output connection.</summary>
        protected bool _isConnected;

        /// <summary>The variable port.</summary>
        protected NodePort _variablePort;

        /// <summary>Whether a valid variable is set.</summary>
        protected bool _isValidVariable;

        /// <summary>Expected type in the variable field.</summary>
        protected System.Type _expectedType = typeof(VariableNode);

        /// <summary>Initialize node for execution.</summary>
        protected override void Init()
        {
            base.Init();
            // Init connected.
            _exit = new FSMConnection(this, "_exit");
            _exit.Init();
            _isConnected = _exit.Connected;
            // Cache type checks.
            _variablePort = GetInputPort("_variable");
            _isValidVariable =
                _variablePort.Connection != null &&
                _expectedType.IsInstanceOfType(_variablePort.Connection.node);
        }

        /// <summary>Get value from a port.</summary>
        public override object GetValue(NodePort port)
        {
            switch (port.fieldName)
            {
                case "_variable":
                    return _isValidVariable ? GetInputValue<object>("_variable", _variable) : null;
                case "_exit":
                    return _exit;
                default:
                    return null;
            }
        }

        /// <summary>Check whether the state needs to execute a transition.</summary>
        public StateNode Check()
        {
            // Make sure we have a valid variable and connection.
            if (!_isConnected || _isValidVariable)
                return null;

            return CheckCondition() != _negate ? _exit.GetState() : null;
        }

        /// <summary>Execute check and return output state on a valid check.</summary>
        protected abstract bool CheckCondition();
    }
}
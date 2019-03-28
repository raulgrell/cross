using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace QAI.FSM
{
    [Serializable]
    public class FSMConnection
    {
        /// <summary>Node to which this connection belongs to.</summary>
        [SerializeField] [HideInInspector] Node _node;

        /// <summary>Node to which this connects to.</summary>
        Node _connected;

        /// <summary>Node port by which the connection is done.</summary>
        [SerializeField] [HideInInspector] string _portName;

        bool _isEmpty;
        bool _isCheckNode;
        bool _isStateNode;

        public string PortName => _portName;
        public bool Connected => _connected;

        public FSMConnection(Node node, string portName)
        {
            _node = node;
            _portName = portName;
        }

        public void Init()
        {
            NodePort port = _node.GetOutputPort(_portName);
            _isEmpty = port?.Connection == null;
            _connected = !_isEmpty ? port.Connection.node : null;
            _isCheckNode = !_isEmpty && _connected.GetType().IsSubclassOf(typeof(ConditionNode));
            _isStateNode = !_isEmpty && _connected.GetType().IsSubclassOf(typeof(StateNode));
        }

        /// <summary>Get the connection state node if any.</summary>
        public StateNode GetState()
        {
            if (_isEmpty)
                return null;

            if (_isCheckNode)
            {
                ConditionNode transition = _connected as ConditionNode;
                return transition.Check();
            }

            if (_isStateNode)
                return _connected as StateNode;

            return null;
        }
    }
}
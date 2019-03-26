using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace QAI.FSM
{
    [NodeTint("#bae1ff")]
    public abstract class StateNode : StateMachineNode
    {
        public bool IsEntry
        {
            get => _isEntry;
            set
            {
                if (value)
                    FSM.SetEntryState(this);
                else
                    FSM.UnsetEntryState(this);

                _isEntry = value;
            }
        }

        [ContextMenu("Set as entry state")]
        public void SetAsRoot()
        {
            IsEntry = true;
        }

        [SerializeField] [HideInInspector] bool _isEntry;
        [SerializeField] [HideInInspector] List<FSMConnection> entries = new List<FSMConnection>();
        [SerializeField] [HideInInspector] List<FSMConnection> exits = new List<FSMConnection>();

        protected override void Init()
        {
            base.Init();
            foreach (FSMConnection connection in exits)
                connection?.Init();
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public StateNode GetTransition()
        {
            foreach (FSMConnection connection in exits)
            {
                if (connection == null)
                    continue;
                return connection.GetState();
            }

            return null;
        }

        public int EntriesCount => entries.Count;

        public FSMConnection GetEntryConnection(int index)
        {
            return entries.Count > index ? entries[index] : null;
        }

        public void AddEntryConnection(NodePort connection)
        {
            if (connection.ValueType != typeof(FSMConnection))
                return;

            NodePort newport = AddInstanceInput(typeof(FSMConnection), ConnectionType.Override, TypeConstraint.None, connection.node.name);
            entries.Add(new FSMConnection(this, newport.fieldName));
            newport.Connect(connection);
        }

        public void RemoveEntryConnection(int index)
        {
            if (entries.Count <= index)
                return;

            RemoveInstancePort(entries[index].PortName);
            entries.RemoveAt(index);
        }

        public int ExitsCount => exits.Count;

        public FSMConnection GetExitConnection(int index)
        {
            return exits.Count > index ? exits[index] : null;
        }

        public void AddExitConnection(NodePort connection)
        {
            if (connection.ValueType != typeof(FSMConnection))
                return;

            NodePort newport = AddInstanceOutput(typeof(FSMConnection), Node.ConnectionType.Override, TypeConstraint.None, connection.node.name);
            exits.Add(new FSMConnection(this, newport.fieldName));
            newport.Connect(connection);
        }

        public void RemoveExitConnection(int index)
        {
            if (exits.Count <= index)
                return;

            RemoveInstancePort(exits[index].PortName);
            exits.RemoveAt(index);
        }
    }
}
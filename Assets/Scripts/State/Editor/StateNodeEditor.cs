using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace QAI.FSM
{
    [CustomNodeEditor(typeof(StateNode))]
    public class StateNodeEditor : NodeEditor
    {
        NodePort _entry;
        NodePort _exit;
        StateNode _state;

        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            _state = target as StateNode;
            OnTransitionsGUI();
        }

        /// <summary>Draw transition ports.</summary>
        protected virtual void OnTransitionsGUI()
        {
            // Only work the GUI for the current exits and entries.
            int exitCount = _state.ExitsCount;
            int entryCount = _state.EntriesCount;

            // Check if we need to create new entry.
            _entry = target.GetInputPort("_entry") 
                     ?? target.AddInstanceInput(typeof(FSMConnection), Node.ConnectionType.Override,
                         Node.TypeConstraint.None, "_entry");

            // If entry connection is not empty create new entry.
            if (_entry.Connection != null)
            {
                _state.AddEntryConnection(_entry.Connection);
                _entry.Disconnect(_entry.Connection);
            }

            // Check if we need to create new exit.
            _exit = target.GetOutputPort("_exit")
                    ?? target.AddInstanceOutput(typeof(FSMConnection), Node.ConnectionType.Override,
                        Node.TypeConstraint.None, "_exit");

            // If exit connection is not empty create new exit.
            if (_exit.Connection != null)
            {
                _state.AddExitConnection(_exit.Connection);
                _exit.Disconnect(_exit.Connection);
            }

            GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(_entry, GUILayout.Width(50));
            EditorGUILayout.Space();
            NodeEditorGUILayout.PortField(_exit, GUILayout.Width(50));
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
            for (int i = 0; i < entryCount; i++)
            {
                FSMConnection connection = _state.GetEntryConnection(i);
                NodePort port = target.GetInputPort(connection.PortName);
                NodePort connected = port.Connection;

                if (connected == null)
                {
                    _state.RemoveEntryConnection(i);
                    i--;
                    entryCount--;
                }
                else
                {
                    GUILayout.BeginHorizontal();
                    NodeEditorGUILayout.PortField(new GUIContent(), port, GUILayout.Width(-4));
                    EditorGUILayout.LabelField(string.Format("{0}", connected.node.name));
                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.Space();

            for (int i = 0; i < exitCount; i++)
            {
                FSMConnection connection = _state.GetExitConnection(i);
                NodePort port = target.GetOutputPort(connection.PortName);
                NodePort connected = port.Connection;

                if (connected == null)
                {
                    _state.RemoveExitConnection(i);
                    i--;
                    exitCount--;
                }
                else
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.Space();
                    NodeEditorGUILayout.PortField(new GUIContent(), port, GUILayout.Width(50));
                    GUILayout.EndHorizontal();
                }
            }
        }

        public override Color GetTint()
        {
            if (Selection.activeGameObject == null)
                return base.GetTint();

            AIGraphRunner runner = Selection.activeGameObject.GetComponent<AIGraphRunner>();
            if (runner == null || _state == null)
                return base.GetTint();

            // Check if it is running.
            StateMachineGraph graph = _state.graph as StateMachineGraph;
            StateNode current = runner.GetStateFromBlackboard(graph.GetInstanceID()) as StateNode;
            return current == _state ? Color.yellow : base.GetTint();
        }
    }
}
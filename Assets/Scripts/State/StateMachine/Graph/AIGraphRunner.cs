using System;
using UnityEngine;
using StateNode = QAI.FSM.StateNode;

namespace QAI
{
    public class AIGraphRunner : MonoBehaviour
    {
        public event Action OnGraphInit;
        
        [SerializeField] protected AIGraph graph;
        protected Blackboard blackboard;
        
        public bool Initialized => blackboard;

        public AIGraph Graph
        {
            set
            {
                graph = value;
                
                if (value == null)
                {
                    blackboard = null;
                    return;
                }

                InitBlackboard();
            }
        }
        
        void Start()
        {
            if (graph != null)
                Graph = graph;
        }

        void Update()
        {
            if (!blackboard)
                return;

            graph.Run(blackboard);
        }

        protected void InitBlackboard()
        {
            blackboard = ScriptableObject.CreateInstance<Blackboard>();
            SetInBlackboard("GameObject", gameObject);
            
            graph.Init(blackboard);
            OnGraphInit?.Invoke();
        }

        public void SetInBlackboard(string key, object value)
        {
            if (blackboard == null) return;
            blackboard.variables[key] = value;
        }

        public void UnsetInBlackboard(string key)
        {
            if (blackboard == null) return;
            blackboard.variables.Remove(key);
        }

        public object GetFromBlackboard(string key)
        {
            if (blackboard == null)
                return null;

            blackboard.variables.TryGetValue(key, out object value);
            return value;
        }
        
        public object GetStateFromBlackboard(int key)
        {
            if (blackboard == null)
                return null;

            blackboard.states.TryGetValue(key, out object value);
            return value;
        }
    }
}
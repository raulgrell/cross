using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace QAI
{
    public abstract class AIGraph : NodeGraph
    {
        protected Blackboard _blackboard;
        public Blackboard Blackboard => _blackboard;

        public T GetValue<T>(string key)
        {
            if (_blackboard == null)
                return default;

            if (_blackboard.variables.TryGetValue(key, out object value))
                return value.GetType().IsAssignableFrom(typeof(T)) ? (T) value : default;

            return default;
        }

        public abstract void Init(Blackboard blackboard);

        public abstract Result Run(Blackboard blackboard);

        public abstract class Result
        {
        }
    }
}
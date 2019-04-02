using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue/Graph", order = 0)]
    public class StateGraph : NodeGraph
    {
        internal Chat current;

        public void Restart()
        {
            current = nodes.Find(c => c is Chat && c.Inputs.All(i => !i.IsConnected)) as Chat;
        }

        public Chat AnswerQuestion(int i)
        {
            current.AnswerQuestion(i);
            return current;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
    [NodeTint("#FFFFAA")]
    public class Event : DialogueBaseNode
    {
        // UnityEvent does not serialize correctly on custom EditorWindows
        public SerializableEvent[] trigger; 

        public override void Trigger()
        {
            foreach (SerializableEvent t in trigger)
                t.Invoke();
        }
    }
}
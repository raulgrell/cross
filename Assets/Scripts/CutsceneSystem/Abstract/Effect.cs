using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Effect : ScriptableObject
{
    public abstract void Setup(PanelData image);
    public abstract void Apply(PanelData image);

}
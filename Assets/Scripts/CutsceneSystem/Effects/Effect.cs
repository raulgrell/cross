using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void Setup(CutscenePanel panel);
    public abstract void Apply(CutscenePanel panel);

}
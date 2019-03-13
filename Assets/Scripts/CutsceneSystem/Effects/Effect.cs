using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void Setup(CutscenePanel panel, GameObject gameObject);
    public abstract void Apply(CutscenePanel panel, GameObject gameObject);
}
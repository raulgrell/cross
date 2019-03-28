using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Units/Data")]
public class UnitData : ScriptableObject
{
    public StateReference state;
    public FloatReference value;
    public IntReference counter;
}

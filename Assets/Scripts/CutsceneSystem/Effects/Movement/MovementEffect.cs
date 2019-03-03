using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene/Effects/MovementEffect")]
public class MovementEffect : Effect
{
    [SerializeField]
    private Vector3 initialPos;
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private float speed;

    public override void Setup(CutscenePanel panel)
    {
        panel.transform.localPosition = initialPos;
    }

    public override void Apply(CutscenePanel panel)
    {
        panel.transform.localPosition = Vector3.MoveTowards(panel.transform.localPosition, panel.transform.localPosition + target, speed);
    }

}

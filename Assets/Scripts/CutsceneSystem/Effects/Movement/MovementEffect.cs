using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene/Effects/Movement")]
public class MovementEffect : Effect
{
    [SerializeField]
    private Vector3 initialPos;
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private float speed;

    public override void Setup(CutscenePanel panel, GameObject gameObject)
    {
        gameObject.transform.localPosition = initialPos;
    }

    public override void Apply(CutscenePanel panel,GameObject gameObject)
    {
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, target, speed);
    }

}

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

    public Vector3 getInicialPos => initialPos;

    public override void Setup(PanelData image)
    {
        image.setInicialPosition(initialPos);
    }

    public override void Apply(PanelData image)
    {
        image.ApplyEffects(target, speed);
    }

}

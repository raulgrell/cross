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
    private float duration;

    //TODO: Easing effects;

    //Step = CurrentTime / TotalDuration
    //EasedStep = Easing.EaseIn(Step, EasingType.Quadratic)
    //CurrentPosition = Lerp(OldPosition, NewPosition, EasedStep)
    // every update, provided a delta time for that frame
    //    sinceStarted += deltaTime;
    //    float step = Math.Min(sinceStarted / duration, 1.0);
    //    float easedStep = Easing.EaseIn(step, EasingType.Quadratic);
    //    float easedValue = source + (dest - source) * easedStep;
    //    // or...
    //    float easedValue = MathHelper.Lerp(source, dest, easedStep);


    public override void Setup(CutscenePanel panel, GameObject gameObject)
    {
        gameObject.transform.localPosition = initialPos;
    }

    public override void Apply(CutscenePanel panel,GameObject gameObject)
    {
        float time =+ Time.deltaTime;
        float step = time / duration;
        gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, target, step);

    //    gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, target, speed);
    }

}

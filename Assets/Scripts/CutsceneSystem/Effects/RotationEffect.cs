using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Cutscene/Effects/Rotation")]
public class RotationEffect : Effect
{
    [SerializeField]
    private Vector3 initialRotation;
    [SerializeField]
    private Vector3 rotationVector;
    [SerializeField]
    private Quaternion rotationQuaternion;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private bool aroundPanel;

    public override void Setup(CutscenePanel panel, GameObject gameObject)
    {
        gameObject.transform.localEulerAngles = initialRotation;
    }
    public override void Apply(CutscenePanel panel, GameObject gameObject)
    {
        if (aroundPanel)
            gameObject.transform.RotateAround(panel.transform.position, rotationVector, rotationSpeed);
        else
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, gameObject.transform.rotation * rotationQuaternion, rotationSpeed); ;
    }

}

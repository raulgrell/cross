using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraMovement : MonoBehaviour
{
    public Transform model;
    private Transform target;
    Vector3 angle;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        angle = model.localEulerAngles;
        

    }

    // Update is called once per frame
    void Update()
    {
        //  Ray ray = new Ray(cameraTrans.position, cameraTrans.position - target.position);
        if (target) {
            var xDistance = target.position.x - model.position.x;
            var zDistance = target.position.z - model.position.z;
            float current = Mathf.Atan2(xDistance, zDistance) * Mathf.Rad2Deg;
            var targetAngle = target.eulerAngles.y;
            var result = (current - targetAngle);
            angle.y = result + 180;
        }
        model.localEulerAngles = angle;
    }
}

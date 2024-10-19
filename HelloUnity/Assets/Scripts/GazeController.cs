using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public Transform target;
    public Transform leftEye;
    public Transform rightEye;
    public Transform leftEar;
    public Transform rightEar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(leftEye);
        Move(rightEye);
        Move(leftEar);
        Move(rightEar);

        Debug.DrawLine(leftEye.position, target.position, Color.blue);
        Debug.DrawLine(rightEye.position, target.position, Color.blue);
    }

    void Move(Transform joint)
    {
        Vector3 r = joint.forward; // current direction of the eyes
        Vector3 e = (target.position - joint.position).normalized; // to target
        Vector3 axisOfRotation = Vector3.Cross(r, e);
        float dotProduct = Vector3.Dot(r, e);
        float angle = Mathf.Atan2(axisOfRotation.magnitude,
            Vector3.Dot(r, r + e));
        // normalize the axis of rotation
        if (axisOfRotation.magnitude > 0.001f)
        {
            axisOfRotation.Normalize();
        }

        // apply the rotation
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle,
            axisOfRotation);
        joint.rotation = rotation * joint.rotation;
    }
}

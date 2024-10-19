using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoLinkController : MonoBehaviour
{
    public Transform target;
    public Transform endEffector;
    public Transform middleJoint;
    public Transform ballJoint;

    private float upperTailLength;
    private float lowerTaillength;

    // Start is called before the first frame update
    void Start()
    {
        upperTailLength = Vector3.Distance(ballJoint.position, middleJoint.position);
        lowerTaillength = Vector3.Distance(middleJoint.position, endEffector.position);
    }

    // Update is called once per frame
    void Update()
    {
        // compute the distance between the ball joint and the target
        Vector3 targetPosition = target.position;
        Vector3 ballPosition = ballJoint.position;
        Vector3 toTarget = targetPosition - ballPosition;
        float distanceToTarget = toTarget.magnitude;

        /*float totalTailLength = upperTailLength + lowerTaillength;
        if (distanceToTarget > totalTailLength)
        {
            toTarget = toTarget.normalized * totalTailLength;
            targetPosition = ballPosition + toTarget;
        }*/

        float a = upperTailLength;
        float b = lowerTaillength;
        float c = distanceToTarget;

        float cosine = Mathf.Clamp((a * a + b * b - c * c) / (2 * a * b), -1f,
            1f);
        float middleAngle = Mathf.Acos(cosine) * Mathf.Rad2Deg;
        float angelToTarget = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        float bAngle = Mathf.Clamp((a * a + c * c - b * b) / (2 * a * b), -1f,
            1f);
        float bAngleOffset = Mathf.Acos(bAngle) * Mathf.Rad2Deg;
        float ballAngle = angelToTarget - bAngleOffset;

        ballJoint.localRotation = Quaternion.Euler(0, 0, ballAngle);
        middleJoint.localRotation = Quaternion.Euler(0, 0, middleAngle);

        Debug.DrawLine(endEffector.position, target.position, Color.green);
        Debug.DrawLine(middleJoint.position, target.position, Color.green);
        Debug.DrawLine(ballJoint.position, target.position, Color.green);
    }
}

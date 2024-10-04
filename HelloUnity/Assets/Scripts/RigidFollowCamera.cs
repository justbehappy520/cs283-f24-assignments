using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RigidFollowCamera : MonoBehaviour
{
    public Transform target;
    public float hDist = 2.0f;
    public float vDist = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // LateUpdate is called after Update
    private void LateUpdate()
    {
        Vector3 tPos = target.position;
        Vector3 tUp = target.up;
        Vector3 tForward = target.forward;

        // camera position is offset from the target position
        Vector3 eye = tPos - tForward * hDist + tUp * vDist;

        // the direction the camera should point is from the target to the camera position
        Vector3 cameraForward = tPos - eye;

        // set the camera's position and rotation with the new values
        // this code assumes that this code runs in a script attached to the camera
        transform.position = eye;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}

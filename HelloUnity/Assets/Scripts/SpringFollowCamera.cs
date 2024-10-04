using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFollowCamera : MonoBehaviour
{
    public Transform target;
    public float hDist = 3.0f;
    public float vDist = 1.0f;
    public float dampConstant = 0.1f;
    public float springConstant = 0.125f;

    private Vector3 actualPos;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        actualPos = target.position - target.forward * hDist +
            target.up * vDist;
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
        Vector3 idealEye = tPos - tForward * hDist + tUp * vDist;

        if(Input.GetKey(KeyCode.Space))
        {
            actualPos = idealEye;
        }

        // the direction the camera should point is from the target to the camera position
        Vector3 cameraForward = tPos - actualPos;

        // compute the acceleration of the spring and then integrate
        Vector3 displacement = actualPos - idealEye;
        Vector3 springAccel = (-springConstant * displacement) - 
            (dampConstant * velocity);

        // update the camera's velocity based on the spring acceleration
        velocity += springAccel * Time.deltaTime;
        actualPos += velocity * Time.deltaTime;

        // set the camera's position and rotation with the new values
        transform.position = actualPos;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}

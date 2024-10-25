using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour : MonoBehaviour
{
    public Transform[] pointsOfInterest; // array of points of interest
    //public Transform target;
    public float moveSpeed = 2.0f; // movement speed of camera
    public float rotateSpeed = 5.0f; // rotate speed of camera
    private int index; // current point of interst
    private bool isMoving; // check if cam is moving and keep cam moving

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check for key press
        if (Input.GetKey(KeyCode.N))
        {
            isMoving = true;
        }
        if (isMoving)
        {
            MoveToPoint(pointsOfInterest[index]);
            if (index == pointsOfInterest.Length)
            {
                index = 0;
            }
        }
    }

    // function to move cam between points of interest
    private void MoveToPoint(Transform target)
    {
        // move the camera
        Vector3 transformPos = transform.position;
        Vector3 targetPos = target.position;
        transform.position = Vector3.Lerp(transformPos, targetPos, 
            moveSpeed * Time.deltaTime);

        // rotate the camera
        Quaternion transformRot = transform.rotation;
        Quaternion targetRot = target.rotation;
        transform.rotation = Quaternion.Slerp(transformRot, targetRot,
            rotateSpeed * Time.deltaTime);

        // check if the camera is near a point of interest
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            transform.position = targetPos;
            transform.rotation = targetRot;
            isMoving = false;
            index++;
        }
    }
}

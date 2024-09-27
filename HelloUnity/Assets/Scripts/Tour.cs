using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour : MonoBehaviour
{
    public Transform[] pointsOfInterest; // array of points of interest
    public float speed = 2.0f; // speed of cam
    private int currentIndex = 0; // current point of interst
    private bool isMoving = false; // check if cam is moving

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // check for key press
       if (Input.GetKey(KeyCode.N) && !isMoving)
        {
            currentIndex = (currentIndex + 1) % pointsOfInterest.Length;
            isMoving = true; // start moving
        }

       if (isMoving)
        {
            MoveToPoint(pointsOfInterest[currentIndex]);
        }
    }

    // function to move cam between points of interest
    private void MoveToPoint(Transform target)
    {
        transform.position = Vector3.Lerp(transform.position,
            target.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation,
            target.rotation, speed * Time.deltaTime);

        // check if the camera is near a point of interest
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex = (currentIndex + 1) % pointsOfInterest.Length;
            isMoving = !isMoving;
        }
    }
}

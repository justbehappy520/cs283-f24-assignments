using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    // rotation variables
    public float sensX; // sensitivity along the x-axis
    public float sensY; // sensitivity along the y-axis
    float xRotation;
    float yRotation;

    // movement variables
    public float moveSpeed;
    float horiInput; // horizontal keyboard input
    float vertInput; // vertical keyboard input
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // limit rotation to 90

        // rotate camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // move camera
        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        dir = transform.forward * vertInput + transform.right * horiInput;

        transform.position += dir * moveSpeed;
    }
}

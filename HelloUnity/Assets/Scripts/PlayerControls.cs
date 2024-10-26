using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // rotation variables
    public float sensX; // sensitivity along the x-axis
    public float sensY; // sensitivity along the y-axis
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
        yRotation += mouseX;

        // rotate player
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        // move player
        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        dir = transform.forward * vertInput + transform.right * horiInput;

        transform.position += dir * moveSpeed;
    }
}

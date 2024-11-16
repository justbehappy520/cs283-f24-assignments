using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    // rotation variables
    public float sensX; // sensitivty for rotation along x-axis
    public float sensY; // sensitivty for rotation along y-axis
    float yRotate;

    // movement variables
    public float moveSpeed;
    public float gravity = -9.81f; // gravity force
    float yVelocity = 0f; // vertical velocity for gravity
    Vector3 velocity;
    float horiInput; // horizontal keyboard input
    float vertInput; // vertical keyboard input
    Vector3 dir;

    // animation variables
    Animator animator;

    // controller variables
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        yRotate += mouseX;

        // rotate player only around the y-axis
        transform.rotation = Quaternion.Euler(0, yRotate, 0);

        // player movement controls
        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        dir = transform.forward * vertInput + transform.right * horiInput;

        // movement
        Vector3 xVelocity = dir.normalized * moveSpeed;

        // apply gravity
        yVelocity += gravity * Time.deltaTime;
        velocity = new Vector3(xVelocity.x, yVelocity, xVelocity.z);

        controller.Move(velocity * Time.deltaTime);

        // animations
        animator.SetBool("isWalking", xVelocity.magnitude > 0.1f);
    }
}

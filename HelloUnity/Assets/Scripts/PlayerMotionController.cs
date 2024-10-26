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

        // move camera
        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        dir = transform.forward * vertInput + transform.right * horiInput;

        velocity = dir.normalized * moveSpeed;
        controller.Move(velocity * Time.deltaTime);

        // animations
        animator.SetBool("isWalking", velocity.magnitude > 0);
    }
}

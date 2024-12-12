using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterController controller;

    // mmovement variables
    public float speed = 5.0f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    // ground check variables
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded; // flag for ground check

    // ghostie interaction
    public bool isTeleporting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // skip movement if teleporting
        /*if (isTeleporting)
        {
            return;
        }*/

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance,
            groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;
        controller.Move(direction * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float linearSpeed = 25.0f;
    public float rotationSpeed = 15.0f;
    public bool smooth = true; // make camera movement smooth
    public float acceleration = 0.05f;
    private float actSpeed = 0.0f;
    // tracks the last dir of the cam
    private Vector3 lastDir = new Vector3(); 
    // track the last position of the mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotation of the character
        Vector3 deltaMouse = Input.mousePosition - lastMouse;
        // get the current rotation based on mouse movement and speed
        float yRotation = deltaMouse.x * rotationSpeed * Time.deltaTime;
        // apply rotation with Quaternion for smooth rotation
        Quaternion targetRotation = Quaternion.Euler(0,
            transform.eulerAngles.y + yRotation, 0); // only rotate on y
        transform.rotation = targetRotation;
        // update lastMouse for the next frame's calculation
        lastMouse = Input.mousePosition;

        // movement of the character
        Vector3 dir = new Vector3(); // create (0,0,0)
        if (Input.GetKey(KeyCode.W)) dir.z += 1.0f;
        if (Input.GetKey(KeyCode.S)) dir.z -= 1.0f;
        if (Input.GetKey(KeyCode.A)) dir.x -= 1.0f;
        if (Input.GetKey(KeyCode.D)) dir.x += 1.0f;
        // normalize movement for smooth diagonal movement
        dir.Normalize();

        if (dir != Vector3.zero)
        {
            // the character is moving
            if (actSpeed < 1) actSpeed += acceleration * Time.deltaTime;
            else actSpeed = 1.0f;

            lastDir = dir;
        }
        else
        {
            // the character is not moving
            if (actSpeed > 0) actSpeed -= acceleration * Time.deltaTime;
            else actSpeed = 0.0f;
            // stop movement when actSpeed is too small
            lastDir = Vector3.zero;
        }

        if (smooth) transform.Translate(lastDir * linearSpeed * Time.deltaTime);
        else transform.Translate(dir * linearSpeed * Time.deltaTime);
    }
}

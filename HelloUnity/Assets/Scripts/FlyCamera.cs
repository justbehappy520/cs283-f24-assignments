using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlyCamera : MonoBehaviour
{
    public float speed = 50.0f; //max speed of camera
    public float sensitivity = 0.25f; // keep it between 0 and 1
    public bool smooth = true; // make camera movement smooth
    public float acceleration = 0.05f;
    private float actSpeed = 0.0f; // keep it between 0 and 1
    private Vector3 lastDir = new Vector3(); // tracks the last dir of the cam
    // track the last position of the mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotation of the camera
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.y,
            transform.eulerAngles.y + lastMouse.x, 0); // no rotate on z
        transform.eulerAngles = lastMouse;

        lastMouse = Input.mousePosition;

        // movement of the camera
        Vector3 dir = new Vector3(); // create (0,0,0)
        
        if (Input.GetKey(KeyCode.W)) dir.z += 1.0f;
        if (Input.GetKey(KeyCode.S)) dir.z -= 1.0f;
        if (Input.GetKey(KeyCode.A)) dir.x -= 1.0f;
        if (Input.GetKey(KeyCode.D)) dir.x += 1.0f;
        dir.Normalize();

        if (dir != Vector3.zero)
        {
            // the camera is moving
            if (actSpeed < 1) actSpeed += acceleration * Time.deltaTime;
            else actSpeed = 1.0f;

            lastDir = dir;
        } else
        {
            // the camera is not moving
            if (actSpeed > 0) actSpeed -= acceleration * Time.deltaTime;
            else actSpeed = 0.0f;
        }

        if (smooth) transform.Translate(lastDir * speed * Time.deltaTime);
        else transform.Translate(dir * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f; // turning and looking
    public Transform player; // the player character
    
    private float xRotation = 0.0f; // to rotate the camera around the x-axis

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 
            Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 
            Time.deltaTime;

        xRotation -= mouseY; // to ensure rotation goes the right way
        xRotation = Mathf.Clamp(xRotation, -90f, 90); // to ensure cam rotate

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        player.Rotate(Vector3.up * mouseX);
    }
}

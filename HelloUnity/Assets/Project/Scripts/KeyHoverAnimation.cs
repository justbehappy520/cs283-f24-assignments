using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHoverAnimation : MonoBehaviour
{
    public float amplitude = 0.5f; // how high it moves
    public float frequency = 1f; // speed of the movement
    public float rotation = 50.0f; // speed of rotation

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // make the object hover
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + new Vector3(0, offset, 0);

        // rotate the object
        transform.Rotate(Vector3.up * rotation * Time.deltaTime, Space.World);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPathCubic : MonoBehaviour
{
    public Transform[] route;
    private int index;
    private float tParam;
    private float speed = 0.5f;
    private Vector3 position;
    private Vector3 prevPos;
    // for coroutine management
    private bool isMoving;
    public bool DeCasteljau;
    public float rotationSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        tParam = 0.0f;
        isMoving = false;
        StartCoroutine(DoBezier(index));
    }

    // Update is called once per frame
    void Update()
    {

        if (!isMoving)
        {
            index++;
            if (index >= route.Length)
            {
                index = 0;
            }
            StartCoroutine(DoBezier(index));
        }
    }

    IEnumerator DoBezier(int point)
    {
        isMoving = true;
        Vector3 b0 = route[point].GetChild(0).position;
        Vector3 b1 = route[point].GetChild(1).position;
        Vector3 b2 = route[point].GetChild(2).position;
        Vector3 b3 = route[point].GetChild(3).position;
        tParam = 0.0f;
        prevPos = transform.position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;
            if (DeCasteljau)
            {
                position = UseDeCasteljau(b0, b1, b2, b3, tParam);
            }
            else
            {
                position = Mathf.Pow(1 - tParam, 3) * b0 +
                    3 * tParam * Mathf.Pow(1 - tParam, 2) * b1 +
                    3 * Mathf.Pow(tParam, 2) * (1 - tParam) * b2 +
                    Mathf.Pow(tParam, 3) * b3;
                transform.position = position;
                yield return null;
            }

            Vector3 direction = (position - prevPos).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotate = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    targetRotate, Time.deltaTime * rotationSpeed);
            }
            transform.position = position;
            prevPos = position;
        }
        isMoving = false;
    }

    Vector3 UseDeCasteljau(Vector3 b0, Vector3 b1, Vector3 b2, Vector3 b3, float t)
    {
        // first interpolation
        Vector3 b01 = Vector3.Lerp(b0, b1, t);
        Vector3 b12 = Vector3.Lerp(b1, b2, t);
        Vector3 b23 = Vector3.Lerp(b2, b3, t);

        // second interpolation
        Vector3 b012 = Vector3.Lerp(b01, b12, t);
        Vector3 b123 = Vector3.Lerp(b12, b23, t);

        // third interpolation
        Vector3 b0123 = Vector3.Lerp(b012, b123, t);

        return b0123;
    }
}

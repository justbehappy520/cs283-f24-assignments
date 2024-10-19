using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FollowPathLinear : MonoBehaviour
{
    public Transform[] route;
    private int index;
    public float duration = 3.0f;
    public float rotationSpeed = 2.0f;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        isMoving = false;
        StartCoroutine(DoLerp(route[index]));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && Input.GetKey(KeyCode.Space))
        {
            index++;
            if (index >= route.Length)
            {
                index = 0;
            }
            StartCoroutine(DoLerp(route[index]));
        }
    }

    IEnumerator DoLerp(Transform target)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 targetPos = target.position;

        Vector3 direction = (targetPos - startPos).normalized;
        Quaternion lookRotate = Quaternion.LookRotation(direction);
        while (Quaternion.Angle(transform.rotation, lookRotate) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                    lookRotate, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float u = timer / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, u);
            yield return null;
        }
        transform.position = target.position;
        isMoving = false;
    }
}

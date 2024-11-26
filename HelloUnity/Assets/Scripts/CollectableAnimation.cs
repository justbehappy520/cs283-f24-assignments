using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableAnimation : MonoBehaviour
{
    public float floatSpd = 5.0f;
    public float shrinkSpd = 1.0f;
    public float duration = 1.0f;

    private Vector3 originalScale;
    private float timer = 0.0f;
    public bool trigger;

    private void Start()
    {
        // store the initial scale of the object
        originalScale = transform.localScale;
        trigger = false;
    }

    private void Update()
    {
        if (trigger)
        {
            // move up and shrink
            transform.position += Vector3.up * floatSpd * Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero,
                timer / duration);

            // increment timer
            timer += Time.deltaTime;

            // bye-bye object
            if (timer >= duration)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void PlayEffect()
    {
        // reset timer
        timer = 0.0f;
        trigger = true;
    }
}

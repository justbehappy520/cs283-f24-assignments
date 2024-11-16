using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform portal;
    public GameObject player;
    public float delay = 2.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TP());
        }
    }

    IEnumerator TP()
    {
        yield return new WaitForSeconds(delay);
        Vector3 destination = portal.position;
        player.transform.position = destination;
    }
}

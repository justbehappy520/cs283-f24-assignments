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
            Debug.Log("Triggered");
            StartCoroutine(TP());
        }
    }

    IEnumerator TP()
    {
        // temporarily disable collider
        Collider portalCollider = GetComponent<Collider>();
        portalCollider.enabled = false;
        yield return new WaitForSeconds(delay);

        // teleport
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
        Vector3 destination = portal.position;
        Debug.Log("Teleporting player to: " + destination);

        // use character controller move method in case that was a problem
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            player.transform.position = destination;
            cc.enabled = true;
        }
        else
        {
            player.transform.position = destination;
        }

        // re-enable collider
        portalCollider.enabled = true;
    }
}

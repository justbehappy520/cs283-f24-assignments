using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectMechanism : MonoBehaviour
{
    public TextMeshProUGUI keyTracker;
    // public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        // initialize UI text with the starting count
        if (keyTracker != null)
        {
            keyTracker.text = "Status: NOT FOUND";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if the collided object is a collectable
        if (other.CompareTag("Collectable"))
        {
            CollectItem(other.gameObject);
        }
    }

    private void CollectItem(GameObject collectable)
    {
        // increase counter and update UI
        UpdateCollectText();
        Debug.Log("collect");

        // trigger animation
        CollectableAnimation animate = collectable.GetComponent<CollectableAnimation>();
        if (animate != null)
        {
            animate.PlayEffect();
        }
        else
        {
            collectable.SetActive(false);
        }
    }

    private void UpdateCollectText()
    {
        // update UI text
        if (keyTracker != null)
        {
            keyTracker.text = "Status: FOUND";
        }
    }
}

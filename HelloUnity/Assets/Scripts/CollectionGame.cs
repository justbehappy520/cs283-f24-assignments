using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionGame : MonoBehaviour
{
    public int collectCounter { get; private set; }
    public TextMeshProUGUI collectText;
    public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        // initialize UI text with the starting count
        if (collectText != null)
        {
            collectText.text = "nut: " + collectCounter;
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
        collectCounter++;
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

        if (spawner != null)
        {
            spawner.RespawnSingleCollectable(collectable);
        }
    }

    private void UpdateCollectText()
    {
        // update UI text
        if (collectText != null)
        {
            collectText.text = "nut: " + collectCounter;
        }
    }
}

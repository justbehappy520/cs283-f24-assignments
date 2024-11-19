using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float range = 50.0f;
    public int maxNumOfSpawn = 5;
    public float spawnDelay = 1.0f;

    private int currentSpawnCount = 0;

    private List<GameObject> spawnedCollectables = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GradualSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GradualSpawn()
    {
        while (spawnedCollectables.Count < maxNumOfSpawn)
        {
            // spawn new collectable
            GameObject newCollectable = SpawnCollectable();
            spawnedCollectables.Add(newCollectable);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private GameObject SpawnCollectable(GameObject existingCollectable = null)
    {
        // generate random position within a specific range
        Vector3 randomOffset = Random.insideUnitSphere * range;
        randomOffset.y = 0;
        Vector3 spawnPos = transform.position + randomOffset;

        if (existingCollectable == null)
        {
            return Instantiate(prefab, spawnPos, Quaternion.identity);
        }
        else
        {
            // if an existing collectable was passed in, move then reativate it
            existingCollectable.transform.position = spawnPos;
            existingCollectable.SetActive(false); 
            existingCollectable.SetActive(true);
            Debug.Log($"Respawned {existingCollectable.name} at position {spawnPos}");

            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            marker.transform.position = spawnPos;
            marker.transform.localScale = Vector3.one * 0.5f;

            return existingCollectable;
        }
    }

    public void RespawnSingleCollectable(GameObject collectable)
    {
        StartCoroutine(Respawn(collectable));
    }
    private IEnumerator Respawn(GameObject collectable)
    {
        yield return new WaitForSeconds(1.0f); // Wait before respawning

        if (collectable != null && !collectable.activeInHierarchy)
        {
            // Respawn the collectable at a new position
            Debug.Log($"Respawning {collectable.name} at position {collectable.transform.position}");
            SpawnCollectable(collectable);  // Respawn collectable
        }
    }
}

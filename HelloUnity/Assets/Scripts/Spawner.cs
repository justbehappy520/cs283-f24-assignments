using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float range = 5.0f;
    public int maxNumOfSpawn = 6;
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
            existingCollectable.SetActive(true);
            return existingCollectable;
        }
    }

    private IEnumerator Respawn()
    {
        while (true)
        {
            foreach (GameObject collectable in spawnedCollectables)
            {
                if (collectable != null && !collectable.activeInHierarchy)
                {
                    SpawnCollectable(collectable);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}

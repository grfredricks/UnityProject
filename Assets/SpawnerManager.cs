using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject spawnerPrefab; // OBJECT TO SPAWN, I created a secondary smaller spawner 
    public float spawnInterval = 10f;      // Time interval for creating new Spawners
    public Vector3 spawnPosition = Vector3.zero; // Doesn't matter where spawner is placed because it handles positioning spawned enemies

    private void Start()
    {
        // Create initial spawner, 1 enemy every 1 second
        StartCoroutine(SpawnNewSpawner());
    }

    private IEnumerator SpawnNewSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Create a new spawner in order to make the amounts of enemies increase over time
            // Mini spawner will be created every 10 seconds, and spawn 1 enemy every 5 seconds
            GameObject newSpawner = Instantiate(spawnerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemySpawnInterval = 1.0f; // Spawn enemy every second

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", enemySpawnInterval, enemySpawnInterval);
    }

    void SpawnEnemy()
    {
        // spawnPosition will be used to randomize if enemy comes from left or right side (same method as arrow)
        Vector3 spawnPosition;
        // Coin flip to see if on right 
        bool spawnOnRight = Random.Range(0, 2) == 0;

        if (spawnOnRight)
        {
            spawnPosition = new Vector3(9.5f, -2.6f, 0f); // Right side
        }
        else
        {
            spawnPosition = new Vector3(-9.5f, -2.6f, 0f); // Left side
        }

        // Create an enemy for the fated side 
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);


        // Same as arrows, enemies are facing towards the right, so if they spawn on the right side,
        // we need to transform their sprite to be facing the left 
        if (spawnOnRight)
        {
            //simply flip x (horizontal) and maintain others
            newEnemy.transform.localScale = new Vector3(-1 * Mathf.Abs(newEnemy.transform.localScale.x),
                                                        newEnemy.transform.localScale.y,
                                                        newEnemy.transform.localScale.z);
        }
        else
        {
            newEnemy.transform.localScale = new Vector3(Mathf.Abs(newEnemy.transform.localScale.x),
                                                        newEnemy.transform.localScale.y,
                                                        newEnemy.transform.localScale.z);
        }
    }
}

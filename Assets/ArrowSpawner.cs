using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;          // The arrow prefab to spawn
    public float arrowSpawnInterval = 5.0f; // Time between each arrow spawn
    public float arrowSpeed = 3.0f;         // Speed of the arrows (adjusted to 9)

    private AudioManager audioManager; // Reference to AudioManager

    private void Start()
    {
        // Find the AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();

        // Start spawning arrows at regular intervals, 5 sec
        InvokeRepeating("SpawnArrow", arrowSpawnInterval, arrowSpawnInterval);
    }

    void SpawnArrow()
    {
        // This is where the arrow will spawn
        Vector3 spawnPosition;
        // Coinflip to determine if arrow spawns from the right or left side of screen
        bool spawnOnRight = Random.Range(0, 2) == 0;

        if (spawnOnRight)
        {
            spawnPosition = new Vector3(9.5f, 3.7f, 0f); // Right side
        }
        else
        {
            spawnPosition = new Vector3(-9.5f, 3.7f, 0f); // Left side
        }
        // Generate an arrow with the chosen side
        GameObject newArrow = Instantiate(arrowPrefab, spawnPosition, arrowPrefab.transform.rotation);


        // Arrow prefab only faces to the right naturally, so if it spawns on the right it needs to face left
        // Flip the arrow's local scale if spawning on the right side
        if (spawnOnRight)
        {
            newArrow.transform.localScale = new Vector3(-1 * Mathf.Abs(newArrow.transform.localScale.x),
                                                        newArrow.transform.localScale.y,
                                                        newArrow.transform.localScale.z);

            // Change arrow's velocity as well in order to move left
            newArrow.GetComponent<Rigidbody2D>().velocity = Vector2.left * arrowSpeed;
        }
        else
        {
            newArrow.transform.localScale = new Vector3(Mathf.Abs(newArrow.transform.localScale.x),
                                                        newArrow.transform.localScale.y,
                                                        newArrow.transform.localScale.z);

            // Set the arrow's velocity to move right
            newArrow.GetComponent<Rigidbody2D>().velocity = Vector2.right * arrowSpeed;
        }

        // Play the arrow sound
        if (audioManager != null)
        {
            audioManager.PlayArrowSound();
        }
    }
}

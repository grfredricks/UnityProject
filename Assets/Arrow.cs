using UnityEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private AudioManager audioManager; // Reference to AudioManager
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If arrow collides with player:
        if (collision.CompareTag("Player"))
        {
            Debug.Log("SLAIN BY AN ARROW"); // Output how player died
            EditorApplication.isPlaying = false; // Stops play mode in the Unity editor
        }
        else if (collision.gameObject.name == "shieldLeft" && collision.gameObject.activeSelf)
        {
            if (audioManager != null)
            {
                audioManager.PlayBlockSound(); //Block sound effect
            }
            // If the arrow collides with the active left shield, destroy the arrow
            Destroy(gameObject);
        }
        else if (collision.gameObject.name == "shieldRight" && collision.gameObject.activeSelf)
        {
            if (audioManager != null)
            {
                audioManager.PlayBlockSound(); //Block sound effect
            }
            // If the arrow collides with the active right shield, destroy the arrow
            Destroy(gameObject);
        }
    }
}

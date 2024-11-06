using System.Collections;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public GameObject shieldLeft; //Player has a shield explicility for left side 
    public GameObject shieldRight; // and right side
    public float shieldDuration = 1.0f; // Duration for how long each shield is active
    public float shieldCooldown = 1.5f; // Cooldown between activations to prevent spamming

    private bool canActivateShield = true; // In order to check if player is allowed to activate shield. Missing a block will turn this off
    

    private void Start()
    {
        // Shields should not be active at the start, and only active on key press, for duration specified, then deactivated again
        shieldLeft.SetActive(false);
        shieldRight.SetActive(false);

        // Find the AudioManager in the scene
        
    }

    private void Update()
    {
        if (canActivateShield)
        {
            // Activate the left shield when pressing Q
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(ActivateShield(shieldLeft));
            }

            // Activate the right shield when pressing E
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(ActivateShield(shieldRight));
            }
        }
    }

    private IEnumerator ActivateShield(GameObject shield)
    {
        canActivateShield = false; // So you can only activate one shield at a time!!!
        shield.SetActive(true); //Activate
        yield return new WaitForSeconds(shieldDuration); // Wait for shield duration
        shield.SetActive(false); //Deactivate
        yield return new WaitForSeconds(shieldCooldown); // Wait for cooldown duration
        canActivateShield = true; // Player can now shield again
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}

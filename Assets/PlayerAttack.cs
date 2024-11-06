using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    //There were some issues after I adjusted the scale of the player, so this is used to maintain original scale of 1.75 DURING animations
    private Vector3 originalScale;

    public float attackRange = 3.9f; // Range within which the attack can destroy an enemy (changed to 4.5 to accompany slightly early attacks)
    private bool attackingLeft; // To track the attack direction and how the animation will play
    private bool canAttack = true; // Controls whether the player can attack, this will be implemented to prevent spamming

    private ScoreManager scoreManager; // A destroyed enemy game object will increment the score by 1 in this script
    private AudioManager audioManager; // Reference to AudioManager

    // I was having trouble with the player's attack animation going through completely before allowing another attaack
    // This method was found on a forum in order to allow player to attack back to back, as long as there was no miss
    private static readonly int AttackHash = Animator.StringToHash("Attack"); 

    void Start()
    {
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
        audioManager = FindObjectOfType<AudioManager>(); // used for kill sound effect


        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (!canAttack) return; // Prevent attack if cooldown is active (DONT MISS)

        // AttackHash is used to restart the attack animation, when a lot of attacks are made late game
        // it  looks clunky to let animation go through

        // Left attack
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            attackingLeft = true;
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            animator.Play(AttackHash, -1, 0f);
            AttackNearestEnemy();
        }

        // Right attack
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            attackingLeft = false;
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            animator.Play(AttackHash, -1, 0f); // Restart the attack animation
            AttackNearestEnemy();
        }
    }

    void AttackNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        // Suggested from a forum to use this method for my plan to "eliminate CLOSEST enemy on the correct side"
        float closestDistance = Mathf.Infinity;
        Collider2D closestEnemy = null;

        foreach (Collider2D hit in hits)
        {
            // Custom tag "Enemy" for some reason wasn't working so I used the default GameController on enemy prefab
            if (hit.CompareTag("GameController"))
            {
                // Evaluate whether player swung in the right direction
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                bool isEnemyOnCorrectSide = (attackingLeft && hit.transform.position.x < transform.position.x) ||
                                            (!attackingLeft && hit.transform.position.x > transform.position.x);

                // Store closest enemy as "hit", which will destroy them instantly (next call)
                // Need these parameters in order to correctly judge if player is spamming/missing
                if (isEnemyOnCorrectSide && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hit;
                }
            }
        }

        if (closestEnemy != null)
        {
            //Instantly remove the enemy. Death animation is preferred but it clogged visuals having enemies linger 
            Destroy(closestEnemy.gameObject);

            if (audioManager != null)
            {
                audioManager.PlayKillSound();
            }

            if (scoreManager != null)
            {
                scoreManager.AddScore(1); // Increment score when an enemy is slain
            }
        }
        else
        {
            StartCoroutine(AttackCooldown()); // Start cooldown if no enemy was hit
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false; // Disable future attacks. Punish the player for a miss
        yield return new WaitForSeconds(1f); // Wait for 1 second
        canAttack = true; // If player somehow survived, they can now attack again. It is essentially SUPPOSED to be game over if you miss 
    }

}

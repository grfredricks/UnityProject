using UnityEngine;
using UnityEditor;

//SCRIPT USED TO HANDLE BOTH ENEMY RUNNING TOWARDS PLAYER AND ATTACK. IF ATTACK FINISHES: GAME OVER

public class EnemyAttack : MonoBehaviour
{
    public float speed = 2.0f;
    public float stoppingDistance = 1.5f;  // Distance at which the enemy stops before reaching the player (don't overlap player)

    private Transform playerTransform;
    private Animator animator;
    private bool hasAttacked = false; // Used to resemble if the enemy has done a complete attack against the player before dying

    void Start()
    {
        // Method is to have the enemy just run towards the player, tagged player
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Requirements to make NEWLY SPAWNED ENEMIES start their run. The !hasAttacked is used to mimic NOT within range of player yet
        if (playerTransform != null && !hasAttacked)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            // Keep running if not 1.5 units away from player (this is good because it works for either left or right side)
            if (distanceToPlayer > stoppingDistance)
            {
                MoveTowardsPlayer();
            }
            else
            {
                StartAttack();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 targetPosition = new Vector2(playerTransform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        animator.SetBool("isRunning", true); // Ensure the running animation is playing during this time
    }

    void StartAttack()
    {
        hasAttacked = true; // Set to true to prevent further attacks, since it is one hit life for player
        animator.SetBool("isRunning", false); //Stop running animation 
        animator.SetTrigger("Attack"); // Start attack animation

        // Invoke the GameOver function after the attack animation completes
        // Might be better habit to create a player health systme but for this method we can just end game at any instance of damage taken
        float attackAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("GameOver", attackAnimationLength);
    }

    void GameOver()
    {
        //Output how player died
        Debug.Log("YOU WERE SLAIN BY ENEMY BANDIT");
        EditorApplication.isPlaying = false; // Stops play testing in the Unity editor, this is the bare minimum just for demonstration purposes
    }
}

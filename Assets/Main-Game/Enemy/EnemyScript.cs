using System;
using UnityEngine;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour
{
    public float enemyHealth = 100f;
    public NavMeshAgent agent;
    public Transform player;
    public Transform[] waypoints;
    public LayerMask whatIsGround, whatIsPlayer;
    public float sightRange, attackRange, walkPointRange, timeBetweenAttacks;
    public bool playerInSightRange, playerInAttackRange;
    public Animator animator;
    public CapsuleCollider capsuleCollider;

    int waypointIndex;
    bool alreadyAttacked;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent missing on " + gameObject.name);
        }

        if (animator == null)
        {
            Debug.LogError("Animator missing on " + gameObject.name);
        }

        if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider missing on " + gameObject.name);
        }

        player = GameObject.Find("PlayerBody")?.transform;
        if (player == null)
        {
            Debug.LogError("PlayerBody not found in scene!");
        }

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints not assigned for " + gameObject.name);
        }

        agent.isStopped = false;
        animator.SetBool("Walk", false);
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (enemyHealth <= 0)
        {
            return;
        }

        if (playerInSightRange)
        {
            if (playerInAttackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[waypointIndex].position);
            animator.SetBool("Walk", true);
        }
    }

    void ChasePlayer()
    {
        if (player == null)
        {
            return;
        }

        agent.SetDestination(player.position);
        animator.SetBool("Walk", true);
    }

    void AttackPlayer()
    {
        if (alreadyAttacked)
        {
            return;
        }

        animator.SetBool("Walk", false);
        animator.SetTrigger("Attack");
        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Dead");
        agent.isStopped = true;
        Destroy(gameObject, 3f);
    }
}

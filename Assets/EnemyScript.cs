using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float enemyHealth;
    public NavMeshAgent agent;
    public Transform player;
    public GameObject playerPistol;

    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public Animator animator;

    int waypointIndex;
    Vector3 target;
    public Transform[] waypoints;

    public CapsuleCollider capsuleCollider;
    public AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        

        enemyHealth = 100;
        animator.SetBool("walk", false);

        player = GameObject.Find("PlayerBody").transform;
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange || playerInAttackRange)
        {
            AttackPlayer();
        }

        if (enemyHealth > 0)
        {
            if (!playerInAttackRange)
            {
                if (!agent.pathPending && !agent.hasPath && agent.remainingDistance < 0.1f)
                {
                    IterateWaypointIndex();
                    UpdateDestination();
                }
            }
        }
        CheckForPlayerAttacking();
    }

    void UpdateDestination()
    {
        animator.SetBool("walk", true);
        target = waypoints[waypointIndex].position; // Set target to waypoint
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    private void AttackPlayer()
    {
        if (enemyHealth > 0 && playerInAttackRange && !alreadyAttacked)
        {
            animator.SetBool("walk", false);
            animator.SetTrigger("Attack");
            audioSource.Play();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            animator.SetTrigger("Dead");
            agent.isStopped = true;
            Destroy(gameObject, 3f);
        }
    }

    void CheckForPlayerAttacking()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public float viewRadius = 15f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    public float speedWalk = 3.5f;
    public float speedRun = 6f;
    public float startWaitTime = 3f;
    public float timeToRotate = 2f;

    public float shootRange = 40f;
    public float shootCooldown = 0.2f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    public float health = 100f;

    public Animator animator;
    public GameObject player;

    private int currentWaypointIndex = 0;
    private float waitTime;
    private float rotateTime;
    private float shootTimer = 0f;
    private Vector3 lastKnownPlayerPosition;
    private bool playerSpotted;
    private bool isChasing;
    private bool isDead;

    private PlayerScript playerScript;
    private Points pointsSystem;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerScript = player.GetComponent<PlayerScript>();
        pointsSystem = player.GetComponent<Points>();
        waitTime = startWaitTime;
        rotateTime = timeToRotate;

        navMeshAgent.speed = speedWalk;
        navMeshAgent.isStopped = false;

        shootTimer = 0f;

        GoToNextWaypoint();
    }

    void Update()
    {
        if (isDead) return;

        shootTimer += Time.deltaTime;
        DetectPlayer();

        if (playerSpotted)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        foreach (var hit in hits)
        {
            Transform target = hit.transform;
            Vector3 direction = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direction) < viewAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, direction, distance, obstacleMask))
                {
                    playerSpotted = true;
                    isChasing = true;
                    lastKnownPlayerPosition = target.position;
                    return;
                }
            }
        }

        playerSpotted = false;
    }

    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        navMeshAgent.speed = speedRun;
        navMeshAgent.SetDestination(player.transform.position);
        animator.SetBool("walk", true);

        if (distanceToPlayer <= shootRange)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("walk", false);
            LookAtPlayer();
            Shoot();
        }
        else
        {
            navMeshAgent.isStopped = false;
        }

        if (!playerSpotted && distanceToPlayer > viewRadius)
        {
            rotateTime -= Time.deltaTime;
            if (rotateTime <= 0f)
            {
                isChasing = false;
                rotateTime = timeToRotate;
                GoToNextWaypoint();
            }
        }
    }

    void Shoot()
    {
        if (shootTimer >= shootCooldown)
        {
            animator.SetTrigger("Shoot");

            GameObject laser = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = laser.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (player.transform.position - firePoint.position).normalized;
                rb.linearVelocity = direction * projectileSpeed;
            }

            shootTimer = 0f;
        }
    }

    void Patrol()
    {
        navMeshAgent.speed = speedWalk;
        animator.SetBool("walk", true);

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (waitTime <= 0f)
            {
                GoToNextWaypoint();
                waitTime = startWaitTime;
            }
            else
            {
                animator.SetBool("walk", false);
                waitTime -= Time.deltaTime;
            }
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        navMeshAgent.isStopped = false;
        navMeshAgent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        navMeshAgent.isStopped = true;
        if (pointsSystem != null)
        {
            pointsSystem.points += 10f;
        }
        StartCoroutine(DestroyAfterDelay(2.5f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health = 100f;
    Animator animator;

    //Idle
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject bullet;
    public Transform attackPoint;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Start() 
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) 
        { 
            Patroling();
            animator.SetBool("isRunning", false);
        }
        if (playerInSightRange && !playerInAttackRange) 
        {
            ChasePlayer();
            animator.SetBool("isRunning", true);
        }
        if (playerInAttackRange && playerInSightRange) {
            AttackPlayer();
            animator.SetBool("isShooting", true );
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            GameObject enemyBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
            enemyBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 32f, ForceMode.Impulse);
            alreadyAttacked = true;
            Destroy(enemyBullet, 3f);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        // Debug.Log("Enemy health is " + health);
        if (health <= 0f)
        {
            GameController.instance.EnemyKilled();
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

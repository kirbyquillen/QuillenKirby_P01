using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks, timeBetweenSuperAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject superprojectile;

    public float sightRange, attackRange, superattackRange;
    public bool playerInSightRange, playerInAttackRange, playerInSuperAttackRange;

    public AudioSource sfx_charge, sfx_shoot;

    private void Awake()
    {
        player = GameObject.Find("Tank").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSuperAttackRange = Physics.CheckSphere(transform.position, superattackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !playerInSuperAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange && !playerInSuperAttackRange) ChasePlayer();
        if (playerInSightRange && !playerInAttackRange && playerInSuperAttackRange) SuperAttackPlayer();
        if (playerInAttackRange && playerInSightRange && playerInSuperAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude <1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        sfx_shoot.Play();

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void SuperAttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            sfx_charge.Play();
            sfx_shoot.Play();

            Rigidbody rb = Instantiate(superprojectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 5);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}

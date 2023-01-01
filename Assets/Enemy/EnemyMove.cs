using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [HideInInspector] private NavMeshAgent agent;
    [HideInInspector] private Transform Player;
    [HideInInspector] private LayerMask WhatisGround;
    [HideInInspector] private LayerMask WhatisPlayer;

    [HideInInspector] private Vector3 walkPoint;
    private bool iswalkPointSet;
    private float walkPointRange;

    [HideInInspector] private float timeBetweenAttacks;
    private bool isAttacked;

    [HideInInspector] private float sightRange;
    [HideInInspector] private float attackRange;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    [HideInInspector] private GameObject projectile;
    private int health = 100;

    private void Awake()
    {
        Player = GameObject.Find("player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatisPlayer);
        
        if(!playerInSightRange && !playerInAttackRange)
            Patroling();
        if(playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if(playerInSightRange && playerInAttackRange)
            AttackPlayer();
    }

    private void Patroling()
    {
        if (!iswalkPointSet)
            SearchWalkPoint();
        if (iswalkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distenceToWalikPoint = transform.position - walkPoint;

        if (distenceToWalikPoint.magnitude < 1f)
            iswalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomz = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, WhatisGround))
            iswalkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(Player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        
        transform.LookAt(Player);

        if (!isAttacked)
        {
            Rigidbody rd = Instantiate(projectile,transform.position,Quaternion.identity).GetComponent<Rigidbody>();
            rd.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rd.AddForce(transform.up * 8f, ForceMode.Impulse);
            isAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        isAttacked = false;
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}

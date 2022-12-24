using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState 
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;

    public float walkSpeed = 0.4f;
    public float runSpeed = 5f;

    public float chaseDistance = 7f;
    private float currentChaseDistance;
    public float attackDistance = 1.8f;
    public float chaseAfterAttackDistance = 2f;

    public float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
    public float patrolThisTime = 15f;
    private float patrolTimer;

    public float waitBeforeAttack = 2f;
    private float attackTimer;

    private Transform target;

    // Start is called before the first frame update
    void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }

    void Start()
    {
        enemyState = EnemyState.PATROL;
        patrolTimer = patrolThisTime;
        attackTimer = waitBeforeAttack;
        currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyState == EnemyState.PATROL)
        {
            Patrol();
        }

        if(enemyState == EnemyState.CHASE)
        {
            Chase();
        }

        if(enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        patrolTimer += Time.deltaTime;

        if(patrolTimer > patrolThisTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }

        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Walk(true);
        }
        else
        {
            enemyAnimator.Walk(true);
        }

        if(Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnimator.Walk(false);
            enemyState = EnemyState.CHASE;

            //Spotted sound
        }
    }

    void Chase()
    {

    }

    void Attack()
    {

    }

    void SetNewRandomDestination()
    {
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randDirection = Random.insideUnitSphere * randRadius;

        randDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, randRadius, -1);
        navAgent.SetDestination(navHit.position);
    }
}

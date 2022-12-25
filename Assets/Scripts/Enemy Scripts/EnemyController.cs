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
    private Transform target;
    private EnemyAudio enemyAudio;
    private EnemyState enemyState;

    private float currentChaseDistance;
    private float patrolTimer;
    private float attackTimer;

    public float walkSpeed = 0.4f;
    public float runSpeed = 5f;
    public float chaseDistance = 15f;
    public float attackDistance = 1f;
    public float chaseAfterAttackDistance = 1f;
    public float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
    public float patrolThisTime = 15f;
    public float waitBeforeAttack = 2f;

    public GameObject attackPoint;

    void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        enemyAudio = GetComponentInChildren<EnemyAudio>();
    }
    void Start()
    {
        enemyState = EnemyState.PATROL;
        patrolTimer = patrolThisTime;
        attackTimer = waitBeforeAttack;
        currentChaseDistance = chaseDistance;
    }
    void Update()
    {
        if (enemyState == EnemyState.PATROL)
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
        else
        {
            enemyAnimator.Walk(true);
        }

        if(Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnimator.Walk(false);
            enemyState = EnemyState.CHASE;
            enemyAudio.PlayScreamSound();
        }
    }
    void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Run(true);
        }
        else
        {
            enemyAnimator.Run(true);
        }

        if(Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            enemyState = EnemyState.ATTACK;

            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
        else if(Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            enemyAnimator.Run(false);
            enemyState = EnemyState.PATROL;
            patrolTimer = patrolThisTime;

            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
    }
    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        attackTimer += Time.deltaTime;

        if(attackTimer > waitBeforeAttack)
        {
            enemyAnimator.Attack();
            attackTimer = 0;
            enemyAudio.PlayAttackSound();
        }

        if(Vector3.Distance(transform.position, target.position) > attackDistance + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.CHASE;
        }
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
    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
    public EnemyState EnemyState
    {
        get; set;
    }
}

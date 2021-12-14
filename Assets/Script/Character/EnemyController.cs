using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { GUARD, PATROL, CHASE, DEAD }
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private EnemyState enemyState;
    private NavMeshAgent agent;

    private Animator animator;

    [Header("Basic Settings")]
    public float sightRadius;
    private GameObject attackTarget;

    public bool isGuard;

    public float lookAtTime;
    private float remainLookAtTime;

    private float lastAttackTime;

    [Header("Patrol State")]
    public float patrolRange;
    //巡逻点
    private Vector3 wayPoint;

    bool isWalk;
    bool isChase;
    bool isFollow;

    //初始速度 
    private float originSpeed;
    //初始位置
    private Vector3 originPosition;
    private CharacterStats stats;

    private CharacterStats characterStats;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
        characterStats = GetComponent<CharacterStats>();
        originSpeed = agent.speed;
        originPosition = transform.position;
        remainLookAtTime = lookAtTime;
    }

    private void Start()
    {
        if (isGuard)
        {
            enemyState = EnemyState.GUARD;
        }
        else
        {
            enemyState = EnemyState.PATROL;
            GetNewWayPoint();
        }
    }

    private void Update()
    {
        switchState();
        switchAnimation();
        lastAttackTime -= Time.deltaTime;
    }
    void switchState()
    {
        if (FoundPlayer())
        {
            enemyState = EnemyState.CHASE;
            Debug.Log("Enemy found player.");
        }

        switch (enemyState)
        {
            case EnemyState.GUARD:
                agent.speed = originSpeed * 0.5f;
                break;
            case EnemyState.PATROL:
                isChase = false;
                agent.speed = originSpeed * 0.5f;

                if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
                {
                    isWalk = false;
                    if (remainLookAtTime > 0)
                    {
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else
                    {
                        GetNewWayPoint();
                    }

                }
                else
                {
                    isWalk = true;
                    agent.destination = wayPoint;
                }
                break;
            case EnemyState.CHASE:
                agent.speed = originSpeed;

                isWalk = false;
                isChase = true;

                if (!FoundPlayer())
                {
                    //玩家已经拉开距离，脱战
                    isFollow = false;
                    if (remainLookAtTime > 0)
                    {
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else if (isGuard)
                    {
                        enemyState = EnemyState.GUARD;
                    }
                    else
                    {
                        enemyState = EnemyState.PATROL;
                    }
                }
                else
                {
                    isFollow = true;
                    agent.isStopped = false;
                    if (attackTarget != null)
                    {
                        agent.destination = attackTarget.transform.position;
                    }
                }

                if (TargetInAttackRange() || TargetInSkillRange())
                {
                    Debug.Log("Plyer in attack range.");
                    isFollow = false;
                    agent.isStopped = true;
                    if (lastAttackTime < 0)
                    {
                        Debug.Log("lastAttackTime ok.");
                        //攻击间隔
                        lastAttackTime = characterStats.attackData.coolDown;
                        //暴击判断
                        characterStats.isCritical = Random.value < characterStats.attackData.criticalChance;
                        //执行攻击
                        Attack();
                    }
                }
                else
                {
                    Debug.Log("Plyer not in attack range.");
                }
                break;
            case EnemyState.DEAD:
                break;
        }
    }

    void Attack()
    {
        transform.LookAt(attackTarget.transform);
        if (TargetInAttackRange())
        {
            //近身攻击
            animator.SetTrigger("Attack");
        }
        else
        {
            //远程攻击
            animator.SetTrigger("Skill");
        }
    }

    void switchAnimation()
    {
        animator.SetBool("Walk", isWalk);
        animator.SetBool("Chase", isChase);
        animator.SetBool("Follow", isFollow);
        animator.SetBool("Critical", characterStats.isCritical);
    }

    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                return true;
            }
        }
        attackTarget = null;
        return false;
    }


    bool TargetInAttackRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= characterStats.attackData.attackRange;
        }
        else
        {
            return false;
        }

    }

    bool TargetInSkillRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= characterStats.attackData.skillRange;
        }
        else
        {
            return false;
        }
    }


    void GetNewWayPoint()
    {
        remainLookAtTime = lookAtTime;

        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);
        Vector3 randomPoint = new Vector3(originPosition.x + randomX, transform.position.y, originPosition.z + randomZ);

        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}

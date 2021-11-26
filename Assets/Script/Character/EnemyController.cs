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
    bool isWalk;
    bool isChase;
    bool isFollow;

    private float originSpeed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        originSpeed = agent.speed;
    }

    private void Update()
    {
        switchState();
        switchAnimation();
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
                agent.speed = originSpeed / 2;
                break;
            case EnemyState.PATROL:
                agent.speed = originSpeed / 2;
                break;
            case EnemyState.CHASE:
                agent.speed = originSpeed;
                
                isWalk = false;
                isChase = true;

                if (!FoundPlayer())
                {
                    //玩家已经拉拖
                    isFollow = false;
                    if (isGuard)
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
                    if (attackTarget != null)
                    {
                        agent.destination = attackTarget.transform.position;
                    }
                }
                break;
            case EnemyState.DEAD:
                break;
        }
    }

    void switchAnimation()
    {
        animator.SetBool("Walk", isWalk);
        animator.SetBool("Chase", isChase);
        animator.SetBool("Follow", isFollow);
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
}

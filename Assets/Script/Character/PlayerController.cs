using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private GameObject attackTarget;
    private float lastAttackTime;
    private CharacterStats characterStats;

    private bool isDead;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MouseManager.Instance.onMouseClick += MoveToTarget;
        MouseManager.Instance.onEnemyClick += EventAttack;
        GameManager.Instance.RegisterPlayer(characterStats);
    }

    public void MoveToTarget(Vector3 target)
    {
        StopAllCoroutines();
        agent.isStopped = false;
        agent.destination = target;
    }

    public void EventAttack(GameObject target)
    {
        if (target != null)
        {
            attackTarget = target;
            characterStats.isCritical = UnityEngine.Random.value < characterStats.attackData.criticalChance;
            StartCoroutine(MoveToAttackTarget());
        }
    }

    IEnumerator MoveToAttackTarget()
    {
        agent.isStopped = false;
        transform.LookAt(attackTarget.transform);
        while (Vector3.Distance(attackTarget.transform.position, transform.position) > characterStats.attackData.attackRange)
        {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }
        agent.isStopped = true;
        if (lastAttackTime < 0)
        {
            animator.SetBool("Critical", characterStats.isCritical);
            animator.SetTrigger("Attack");
            lastAttackTime = characterStats.attackData.coolDown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isDead = characterStats.CurrentHealth == 0;
        switchAnimation();
        lastAttackTime -= Time.deltaTime;
    }

    private void switchAnimation()
    {
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
        animator.SetBool("Death", isDead);
    }

    //Animation Event
    void Hit()
    {
        if (attackTarget != null)
        {
            var targetStatus = attackTarget.GetComponent<CharacterStats>();
            targetStatus.TakeDamage(characterStats, targetStatus);
        }
    }
}

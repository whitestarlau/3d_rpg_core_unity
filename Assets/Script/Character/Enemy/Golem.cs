using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : EnemyController
{
    [Header("Skill")]
    public float kickForce = 1;

    public void KickOff()
    {
        if (attackTarget != null && transform.IsFacingTarget(attackTarget.transform))
        {
            var targetStatus = attackTarget.GetComponent<CharacterStats>();

            Vector3 direction = attackTarget.transform.position - transform.position;
            direction.Normalize();

            NavMeshAgent attackNav = attackTarget.GetComponent<NavMeshAgent>();
            attackNav.isStopped = true;
            attackNav.velocity = direction * kickForce;

            attackTarget.GetComponent<Animator>().SetTrigger("Dizzy");
            
            targetStatus.TakeDamage(characterStats,targetStatus);
        }
    }
}

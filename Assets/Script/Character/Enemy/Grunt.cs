using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : EnemyController
{
    [Header("Skill")]
    public float kickForce = 1;

    public void KickOff()
    {
        if (attackTarget != null)
        {
            Debug.Log("Grunt KickOff");
            transform.LookAt(attackTarget.transform);
            Vector3 direction = attackTarget.transform.position - transform.position;
            direction.Normalize();

            NavMeshAgent attackNav = attackTarget.GetComponent<NavMeshAgent>();
            attackNav.isStopped = true;
            attackNav.velocity = direction * kickForce;

            attackTarget.GetComponent<Animator>().SetTrigger("Dizzy");

        }
    }
}

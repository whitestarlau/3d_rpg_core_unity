using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rock : MonoBehaviour
{
    public enum RockStates
    {
        HitPlayer, HitEnemy, HitNothing
    }

    private Rigidbody rb;

    public RockStates rockState;

    public float force;

    public int damage;

    public GameObject target;
    private Vector3 direction;

    public GameObject breakEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.one;

        rockState = RockStates.HitPlayer;
        FlyToTarget();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude < 1f)
        {
            rockState = RockStates.HitNothing;
        }
    }

    public void FlyToTarget()
    {
        if (target == null)
        {
            Destroy(this);
            return;
        }
        direction = (target.transform.position - transform.position + Vector3.up).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (rockState)
        {
            case RockStates.HitPlayer:
                if (other.gameObject.CompareTag("Player"))
                {
                    NavMeshAgent navAgent = other.gameObject.GetComponent<NavMeshAgent>();
                    navAgent.isStopped = true;
                    navAgent.velocity = direction * force;

                    other.gameObject.GetComponent<Animator>().SetTrigger("Dizzy");

                    CharacterStats characterStats = other.gameObject.GetComponent<CharacterStats>();
                    characterStats.TakeDamage(damage, characterStats);

                    rockState = RockStates.HitNothing;
                }
                break;

            case RockStates.HitEnemy:
                if (other.gameObject.GetComponent<Golem>())
                {
                    CharacterStats otherStatus = other.gameObject.GetComponent<CharacterStats>();
                    otherStatus.TakeDamage(damage, otherStatus);

                    Instantiate(breakEffect, transform.position, Quaternion.identity);

                    Destroy(gameObject);
                }
                break;
        }
    }
}

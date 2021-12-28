using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Rigidbody rb;
    private Collider collider;

    public float force;

    public GameObject target;
    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        FlyToTarget();
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
}

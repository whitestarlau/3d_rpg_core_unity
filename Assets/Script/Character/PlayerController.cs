using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MouseManager.Instance.onMouseClick += MoveToTarget;
    }

    public void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

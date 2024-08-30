using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Transform target;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        //RotateToTarget();
    }

    private void RotateToTarget()
    {
        //transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerController.instance;
    }
}

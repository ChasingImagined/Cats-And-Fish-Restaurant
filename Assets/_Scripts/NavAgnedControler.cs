using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgnedControler : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    

    private void Awake()
    {
       
    }
    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

       
    }
    private void Update()
    {
        agent.SetDestination(target.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CharcterBase : MonoBehaviour,IQueueable
{
    [SerializeField] Vector3 targetPoziton;
    [SerializeField] NavMeshAgent agent;

    private void Start()
    {
        targetPoziton = this.transform.position;
        agent.updateRotation = false;
        agent.updateUpAxis = false;


    }
    private void Update()
    {
        if (targetPoziton == null) return;
        agent.SetDestination(targetPoziton);
    }


}

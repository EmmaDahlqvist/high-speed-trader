using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIControl : MonoBehaviour
{
    public GameObject goal;
    NavMeshAgent agent;
    Vector3 randomOffset;

    public bool wait;

    void Start()
    {
        if (wait) return;
        Initiate();
    }

    public void Initiate()
    {
        print("starting ai");
        randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goal.transform.position);
        int r = Random.Range(40, 60);
        agent.avoidancePriority = r;
    }

    void Update()
    {
        if (wait) return;
        if (agent.remainingDistance > 10)
            agent.SetDestination(goal.transform.position + randomOffset * 5);
        else
            agent.SetDestination(goal.transform.position + randomOffset);
    }
}

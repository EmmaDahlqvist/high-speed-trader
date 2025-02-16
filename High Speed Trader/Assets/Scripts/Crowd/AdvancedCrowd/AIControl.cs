using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIControl : MonoBehaviour
{
    public GameObject goal;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goal.transform.position);
        agent.speed = Random.Range(7f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(goal.transform.position);
    }
}

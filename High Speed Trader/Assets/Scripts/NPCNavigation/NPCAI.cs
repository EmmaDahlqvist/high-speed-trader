using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    GameObject[] goalLocations;
    NavMeshAgent agent;


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag("Goal");
        GoToNewLocation();
    }

    
    void Update()
    {

        if (agent.remainingDistance < 1)
        {
            GoToNewLocation();
        }
    }

    private void GoToNewLocation()
    {
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);
    }

}

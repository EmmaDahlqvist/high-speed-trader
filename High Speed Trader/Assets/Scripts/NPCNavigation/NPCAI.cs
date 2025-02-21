using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCAI : MonoBehaviour
{
    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator childAnimator;

    [SerializeField] private float minIdleTime = 0f;
    [SerializeField] private float maxIdleTime = 5f;

    private bool isIdle = false;


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
            return;
        }

        goalLocations = GameObject.FindGameObjectsWithTag("NPCWanderGoal");
        if (goalLocations.Length == 0)
        {
            Debug.LogError("No goal locations found with tag 'NPCWanderGoal'");
            return;
        }

        childAnimator = transform.Find("Body")?.GetComponent<Animator>();
        if (childAnimator == null)
        {
            Debug.LogError("Animator component not found on child object 'WorkerAnimated'");
            return;
        }
        childAnimator.SetBool("isIdle", false);
        GoToNewLocation();
    }

    void Update()
    {
        if (!isIdle && agent.remainingDistance <= 1f && !agent.pathPending)
        {
            StartCoroutine(IdleAndMove());
        }
    }

    private IEnumerator IdleAndMove()
    {
        isIdle = true;
        childAnimator.SetBool("isIdle", true);
        agent.isStopped = true;

        float idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);

        childAnimator.SetBool("isIdle", false);
        agent.isStopped = false;
        GoToNewLocation();
        isIdle = false;

    }

    private void GoToNewLocation()
    {
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);
    }
}

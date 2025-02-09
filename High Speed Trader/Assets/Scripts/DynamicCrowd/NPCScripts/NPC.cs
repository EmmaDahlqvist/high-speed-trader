using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[SelectionBase]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class NPC : MonoBehaviour
{

    [HideInInspector]
    public NavMeshAgent Agent;

    [HideInInspector]
    public Animator Animator;

    public bool Wander = false;

    public bool Alive = true;

    public Group Group = null;

    public Vector3 Direction { get; private set; } = Vector3.forward;

    public Vector3 Position => transform.position;


    public float CurrentSpeed
    {
        get { return Agent.velocity.magnitude; }
    }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CurrentSpeed > 0.1f)
        {
            Direction = Agent.velocity.normalized;
        }
    }

}

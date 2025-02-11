using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWander : NPCComponent
{

    private void Start()
    {
        npc.Agent.SetDestination(new Vector3(100, 1.71000004f, 30.8199997f));
    }
    
}

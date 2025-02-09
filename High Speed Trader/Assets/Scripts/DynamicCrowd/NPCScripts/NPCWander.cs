using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWander : NPCComponent
{

    private void Start()
    {
        npc.Agent.SetDestination(new Vector3(20.6900005f, 1.71000004f, 20.8199997f));
    }
    
}

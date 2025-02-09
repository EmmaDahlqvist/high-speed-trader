using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWander : NPCComponent
{

    private void Start()
    {
        npc.Agent.SetDestination(new Vector3(20, 0.000100135803f, 44.5099983f));
    }
    
}

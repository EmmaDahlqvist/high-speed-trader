using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCComponent : MonoBehaviour
{

    protected NPC npc;


    protected virtual void Awake()
    {
        npc = GetComponentInParent<NPC>();
    }

}

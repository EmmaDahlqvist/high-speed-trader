using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Formations/Column Formation")]
public class ColumnFormation : Formation
{
    [SerializeField]
    float Spacing = 3f;


    public override Vector3 GetPosition(NPC npc, Group group)
    {
        if (group.IsLeader(npc))
        {
            return npc.Position;
        }

        NPC leader = group.GetLeader();

        Vector3 position = leader.Position - leader.Direction * Spacing * group.Members.IndexOf(npc);

        
        return AdjustPosition(position, leader.Position);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Formations/Rectangle Formation")]
public class RectangleFormation : Formation
{
    [SerializeField]
    float Spacing = 3f;

    [SerializeField]
    int side1 = 3;

    [SerializeField]
    int side2 = 2;


    public override Vector3 GetPosition(NPC npc, Group group)
    {
        if (group.IsLeader(npc))
        {
            return npc.Position;
        }


        

        NPC leader = group.GetLeader();

        int index = group.Members.IndexOf(npc);

        Debug.Log($"I am . leader is {leader}, index is {index}");

        Vector3 offset = new Vector3((index % side1) * Spacing, (index % side2) * Spacing);
        


        Vector3 position = leader.Position + offset;
        


        // Vector3 position = leader.Position - leader.Direction * Spacing * group.Members.IndexOf(npc);


        return AdjustPosition(position, leader.Position);
    }
}

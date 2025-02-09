using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{

    public bool DrawGizmos = false;

    [SerializeField]
    private List<NPC> members = new List<NPC>();

    public List<NPC> Members => members;

    [SerializeField]
    public Formation Formation;


    public int MemberCount
    {
        get { return members.Count;}
    }

    public int FollowerCount
    {
        get { return Mathf.Max(0, MemberCount - 1); }
    }

    public int GetFollowerIndex(NPC npc)
    {
        return members.IndexOf(npc) - 1;
    }

    public bool IsLeader(NPC npc)
    {
        int index = members.IndexOf(npc);

        return index == 0;
    }

    public NPC GetLeader()
    {
        if (members.Count >= 1)
        {
            return members[0];
        }

        return null;
    }

    public Vector3 GetPositionInGroup(NPC npc)
    {
        return Formation.GetPosition(npc, this);
    }


    #region Unity Method


    void Start()
    {
        foreach (NPC member in members)
        {
            member.Group = this;
        }
    }

    private void OnDrawGizmos()
    {
        if (Formation == null || MemberCount <= 0 || DrawGizmos == false)
        {
            return;
        }

        foreach (NPC member in members)
        {
            Vector3 position = GetPositionInGroup(member);

            Gizmos.color = Color.green;

            if (IsLeader(member))
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawSphere(position, 0.5f);
        }
    }    

    private void Update()
    {

        for (int i = members.Count - 1; i >= 0; i--)
        {
            if (members[i].Alive == false)
            {
                members[i].Group = null;

                members.RemoveAt(i);
            }
        }
    }

    #endregion

    #region Members Management Methods

    public void AddMember(NPC member)
    {
        members.Add(member);

        member.Group = this;
    }

    public void RemoveMember(NPC member)
    {
        members.Remove(member);

        member.Group = null;
    }

    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{

    [SerializeField]
    List<Group> Groups = new List<Group>();


    [ContextMenu("Create Group")]

    public Group CreateGroup()
    {
        GameObject obj = new GameObject($"Group {Groups.Count + 1}");

        obj.transform.parent = transform;

        Group group = obj.AddComponent<Group>();

        Groups.Add( group );

        return group;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.red; // Färg på Gizmo
    public float gizmoSize = 0.5f; // Storlek på Gizmo-sfären

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoSize);
    }
}

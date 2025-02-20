using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField]
    private Transform pointA;

    [SerializeField]
    private Transform pointB;

    [SerializeField]
    private Transform pointC;

    [SerializeField]
    private Transform pointD;

    [SerializeField]
    private Transform pointA1;

    [SerializeField]
    private Transform pointB1;

    [SerializeField]
    private Transform pointC1;

    [SerializeField]
    private Transform pointD1;

    [SerializeField]
    private Transform movingPoint;

    [SerializeField]
    private float interpolateAmount;

    [SerializeField]
    private Transform[] current_points;

    [SerializeField]
    private Transform[] next_points;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float maxMoveAmount;


    private void Update()
    {
        interpolateAmount = (interpolateAmount + Time.deltaTime * speed) % maxMoveAmount;
        // interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;

        movingPoint.position = CubicLerp(current_points[0].position, current_points[1].position, current_points[2].position, current_points[3].position, interpolateAmount);
        // movingPoint.position = CubicLerp(pointA.position, pointB.position, pointC.position, pointD.position, interpolateAmount);

        
        if (Vector3.Distance(movingPoint.position, current_points[3].position) == 0)
        {
            Transform[] tmp_points = current_points;
            current_points = next_points;
            next_points = tmp_points;
            movingPoint.position = current_points[0].position;
        }
        
    }


    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, interpolateAmount);
    }

    private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab_bc = QuadraticLerp(a, b, c, t);
        Vector3 bc_cd = QuadraticLerp(b, c, d, t);

        return Vector3.Lerp(ab_bc, bc_cd, interpolateAmount);
    }
    


}

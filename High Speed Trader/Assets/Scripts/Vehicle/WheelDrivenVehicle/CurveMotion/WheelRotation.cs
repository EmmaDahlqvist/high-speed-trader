using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public Transform[] wheels;
    public float wheelRadius = 0.3f;

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float distanceTraveled = Vector3.Distance(transform.position, lastPosition);
        float rotationAngle = (distanceTraveled / (2 * Mathf.PI * wheelRadius)) * 360f;

        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right, rotationAngle, Space.Self);
        }

        lastPosition = transform.position;
    }
}

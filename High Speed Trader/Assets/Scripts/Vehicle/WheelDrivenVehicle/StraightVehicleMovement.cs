using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 Vehiclemovement such that it constantly accelerates, no steering
 */
public class StraightVehicleMovement: MonoBehaviour
{
    public float motorTorque = 2000;
    public float brakeTorque = 2000;
    public float maxSpeed = 20;
    public float centreOfGravityOffset = -1f;

    WheelControl[] wheels;
    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        wheels = GetComponentsInChildren<WheelControl>();
    }

    void Update()
    {
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.velocity);

        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        foreach (var wheel in wheels)
        {
            if (wheel.motorized)
            {
                wheel.WheelCollider.motorTorque = currentMotorTorque;
            }
            wheel.WheelCollider.brakeTorque = 0;
        }
    }
}

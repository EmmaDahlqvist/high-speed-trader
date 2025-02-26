using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePropeller : MonoBehaviour
{
    public GameObject propellerObj; // Propellerobjektet
    public float rotationSpeed = 360f; // Grader per sekund

    void Update()
    {
        // Roterar propellern smidigt runt sin lokala Z-axel i en j�mn hastighet
        propellerObj.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}

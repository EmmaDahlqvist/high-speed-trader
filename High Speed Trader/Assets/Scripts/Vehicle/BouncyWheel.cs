using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyWheel : MonoBehaviour
{

    public float bounceMultiplier = 1.5f; // �ka studs varje g�ng

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Abs(rb.velocity.y) * bounceMultiplier, rb.velocity.z);
        }
    }
   
}

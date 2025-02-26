using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePosition : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform cameraOrientation;
    public float offsetDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position + cameraOrientation.forward * offsetDistance;
        transform.rotation = cameraOrientation.rotation;
    }
}

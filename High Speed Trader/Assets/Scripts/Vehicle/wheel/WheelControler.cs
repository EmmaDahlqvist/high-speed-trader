using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public Transform wheelModel;

    [HideInInspector] public WheelCollider WheelCollider;

    public bool steerable;
    public bool motorized;
    public bool easteregg = false;

    Vector3 position;
    Quaternion rotation;

    private void Start()
    {
        WheelCollider = GetComponent<WheelCollider>();
    }

    void Update()
    {
        
        WheelCollider.GetWorldPose(out position, out rotation);
        if (easteregg) return;
        wheelModel.transform.position = position;
        wheelModel.transform.rotation = rotation;
    }
}

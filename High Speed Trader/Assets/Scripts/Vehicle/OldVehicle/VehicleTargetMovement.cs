using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleTargetMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Vector3 targetPoint;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveVehicle();
    }

    private void MoveVehicle()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

}

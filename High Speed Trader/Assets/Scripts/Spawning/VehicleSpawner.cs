using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [Header("Vehicle Settings")]
    public GameObject vehiclePrefab;
    public float rotationOnSpawn;

    [Header("Spawn Settings")]
    public float spawnTimer;

    private Quaternion rotationQuaternion;


    void Start()
    {
        rotationQuaternion = Quaternion.AngleAxis(rotationOnSpawn, Vector3.up);
        StartCoroutine(vehicleWave());
    }

    private void spawnVehicle()
    {
        
        GameObject vehicle = Instantiate(vehiclePrefab, transform.position, rotationQuaternion) as GameObject;
    }

    IEnumerator vehicleWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            spawnVehicle();
        }
    }
}

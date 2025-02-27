using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneSpawnZone : MonoBehaviour
{
    public GameObject airplane;

    private void Start()
    {
        airplane.SetActive(false);   
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spawing airplane");
        airplane.SetActive(true);
    }
}

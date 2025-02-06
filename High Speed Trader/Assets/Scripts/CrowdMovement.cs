using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CrowdMovement : MonoBehaviour
{

    [Header("PathInformation")]
    public Transform[] waypoints;


    [Header("Movement")]
    public float crowdSpeed;


    private int currentWaypointIndex = 0;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, crowdSpeed * Time.deltaTime);

        if (checkReachedWaypoint(targetWaypoint))
        {
            currentWaypointIndex++;

            faceNewWaypoint();

            handleLastWayPoint();
        }

    }

    private void faceNewWaypoint()
    {
        // TODO: implementera detta
    }

    private void handleLastWayPoint()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = waypoints.Length - 1;
        }
    }

    private bool checkReachedWaypoint(Transform targetWaypoint)
    {
        return Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

}

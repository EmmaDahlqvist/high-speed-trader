using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneWaypoint : MonoBehaviour
{
    public List<GameObject> wayPoints;
    public float speed = 2;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = wayPoints[index].transform.position;

        Vector3 newPos = Vector3.MoveTowards(transform.position, wayPoints[index].transform.position, speed*Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);

        if(distance <= 0.05)
        {
            if(index < wayPoints.Count-1)
                index++;
        }
    }
}

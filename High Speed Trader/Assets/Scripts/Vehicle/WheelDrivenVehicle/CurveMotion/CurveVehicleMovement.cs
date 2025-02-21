using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveVehicleMovement : MonoBehaviour
{
    
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector3 carPosition;

    [SerializeField]
    private float speedModifier;

    private bool coroutineAllowed;


    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }


    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            carPosition = Mathf.Pow(1- tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = carPosition;

            Vector3 tangent =
                3 * Mathf.Pow(1 - tParam, 2) * (p1 - p0) +
                6 * (1 - tParam) * tParam * (p2 - p1) +
                3 * Mathf.Pow(tParam, 2) * (p3 - p2);

            if (tangent != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(tangent);
            }


            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
            routeToGo = 0;

        coroutineAllowed = true;
    }
    
}

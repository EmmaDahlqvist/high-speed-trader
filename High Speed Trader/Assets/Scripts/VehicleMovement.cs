using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float distance;

    private Vector3 startPos;
    private float direction = 1;
    private float turnCooldown;
    private bool readyToTurn;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        turnCooldown = 0.5f;
        readyToTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveVehicle();
    }

    private void MoveVehicle()
    {
        transform.position += Vector3.right * moveSpeed * direction * Time.deltaTime;

        if ( CheckBounds() && readyToTurn )
        {
            readyToTurn = false;
            Turn();
            Invoke(nameof(ResetTurn), turnCooldown);
        }
    }

    private bool CheckBounds()
    {
        return Mathf.Abs(transform.position.x - startPos.x - 1f) >= distance;
    }
    
    private void ResetTurn()
    {
        readyToTurn = true;
    }

    private void Turn()
    {
        direction *= -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}

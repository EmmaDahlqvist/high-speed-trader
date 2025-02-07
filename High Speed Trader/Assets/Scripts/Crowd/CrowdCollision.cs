using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrowdCollision : MonoBehaviour
{
    public float destroyDelay = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.transform.root.gameObject, destroyDelay);
        }
    }
}

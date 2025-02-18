using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    
    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(4, LoadSceneMode.Single);
            await Task.Delay(1); // Delay for 1 millisecond
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(4));
            SceneManager.UnloadSceneAsync(currentSceneIndex);
            
        }

        if (other.CompareTag("Obstacle"))
        {
            // Destroy(other.transform.root.gameObject, destroyDelay);
            Destroy(other.transform.parent.gameObject, destroyDelay);
        }
    }
}

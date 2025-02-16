using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayerCollision: MonoBehaviour
{

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(4, LoadSceneMode.Single);
            await Task.Delay(5); // Delay for 1 millisecond
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(4));
            // SceneManager.UnloadSceneAsync(currentSceneIndex);
        }

    }

}

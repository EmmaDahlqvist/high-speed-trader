using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVoidCheck : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField]
    private float voidPosition;

    void Start()
    {
        playerTransform = this.transform;
    }

    void Update()
    {
        HandleVoid();
    }

    private async void HandleVoid()
    {
        if (playerTransform.position.y < voidPosition)
        {
            GameState.KillReason = "You died in void!";
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
            await Task.Delay(5); // Delay for 1 millisecond
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("EndScreen"));
        }
    }

}

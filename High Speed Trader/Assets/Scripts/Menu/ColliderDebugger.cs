using UnityEngine;

public class ColliderDebugger : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Canvas tr�ffades av: " + collision.gameObject.name);
    }
}

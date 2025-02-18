using UnityEngine;

public class ColliderDebugger : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Canvas träffades av: " + collision.gameObject.name);
    }
}

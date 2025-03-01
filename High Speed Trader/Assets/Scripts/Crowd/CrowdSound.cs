using UnityEngine;

public class CrowdAudioController : MonoBehaviour
{
    public Transform player;  // Drag the player GameObject here
    public Transform[] npcs;  // Assign all NPCs to this array
    public float maxVolume = 1.0f;
    public float minVolume = 0.0f;
    public float maxDistance = 20f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            Debug.LogError("No AudioSource found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (!audioSource || !player || npcs.Length == 0) return;

        // Calculate the average position of all NPCs
        Vector3 averagePosition = Vector3.zero;
        foreach (Transform npc in npcs)
        {
            averagePosition += npc.position;
        }
        averagePosition /= npcs.Length;

        // Move the Crowd GameObject to the average position
        transform.position = averagePosition;

        // Adjust volume based on player distance
        float distance = Vector3.Distance(transform.position, player.position);
        float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
        audioSource.volume = Mathf.Clamp(volume, 0.3f, maxVolume); // Ensure a minimum volume
    }
}
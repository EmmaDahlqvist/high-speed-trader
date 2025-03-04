using System.Collections;
using UnityEngine;

public class CrowdSound : MonoBehaviour
{
    public Transform player;
    public Transform[] npcs;
    public float maxVolume = 1.0f;
    public float minVolume = 0.3f;
    public float maxDistance = 20f;
    public AudioClip[] footstepSounds; // Assign multiple footstep sounds
    public int minFootstepsPerSecond = 2;
    public int maxFootstepsPerSecond = 6;

    private AudioSource audioSource;
    private bool isPlayingFootsteps = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            Debug.LogError("No AudioSource found on " + gameObject.name);
        }

        StartCoroutine(PlayFootstepsWithRandomIntervals());
    }

    void Update()
    {
        if (!audioSource || !player || npcs.Length == 0 || footstepSounds.Length == 0) return;

        Vector3 averagePosition = Vector3.zero;
        foreach (Transform npc in npcs)
        {
            averagePosition += npc.position;
        }
        averagePosition /= npcs.Length;

        transform.position = averagePosition;

        // Adjust volume based on player distance
        float distance = Vector3.Distance(transform.position, player.position);
        float volume = maxVolume * (1 - Mathf.Clamp01(distance / maxDistance));
        audioSource.volume = Mathf.Clamp(volume * 3, minVolume, maxVolume);
    }

    IEnumerator PlayFootstepsWithRandomIntervals()
    {
        isPlayingFootsteps = true;
        while (true) // Loop forever
        {
            float interval = 1f / Random.Range(minFootstepsPerSecond, maxFootstepsPerSecond);
            yield return new WaitForSeconds(interval);

            PlayRandomFootstep();
        }
    }

    void PlayRandomFootstep()
    {
        if (footstepSounds.Length == 0) return;

        AudioClip randomClip = footstepSounds[Random.Range(0, footstepSounds.Length)];
        audioSource.pitch = Random.Range(0.8f, 1.2f); // Add pitch variation
        audioSource.PlayOneShot(randomClip, audioSource.volume * Random.Range(0.6f, 1.0f));
    }
}

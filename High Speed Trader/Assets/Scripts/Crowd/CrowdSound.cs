using System.Collections;
using UnityEngine;

public class CrowdSound : MonoBehaviour
{
    public Transform player;
    public Transform[] npcs;
    public float maxVolume = 1.0f;
    public float minVolume = 0.3f;
    public float peakDistance = 15f; // Distance where volume is highest
    public float maxDistance = 30f; // Beyond this, volume is minimal
    public AudioClip[] footstepSounds;
    public int minFootstepsPerSecond = 2;
    public int maxFootstepsPerSecond = 6;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();


        StartCoroutine(PlayFootstepsWithRandomIntervals());
    }

    void Update()
    {
        if (!audioSource || !player || npcs.Length == 0 || footstepSounds.Length == 0) return;

        Transform closestNpc = null;
        float closestDistance = float.MaxValue;

        foreach (Transform npc in npcs)
        {
            float distance = Vector3.Distance(npc.position, player.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNpc = npc;
            }
        }

        if (closestNpc != null)
        {
            transform.position = closestNpc.position;
           
        }

        // Adjust volume so it's loudest at peakDistance
        float volume = 0f;

        if (closestDistance <= peakDistance)
        {
            // Increase volume as NPC gets closer to peak distance
            volume = Mathf.Lerp(minVolume, maxVolume, closestDistance / peakDistance);
        }
        else if (closestDistance > peakDistance && closestDistance <= maxDistance)
        {
            // Decrease volume past the peak distance
            volume = Mathf.Lerp(maxVolume, minVolume, (closestDistance - peakDistance) / (maxDistance - peakDistance));
        }
        else
        {
            // Beyond maxDistance, keep at min volume
            volume = minVolume;
        }

        audioSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);

    }

    IEnumerator PlayFootstepsWithRandomIntervals()
    {
        while (true)
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
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(randomClip, audioSource.volume * Random.Range(0.8f, 1.2f));
    }
}

using UnityEngine;

public class CarAudioController : MonoBehaviour
{
    public AudioSource engineSound;
    public Transform player;

    public float sweetSpotDistance = 10f; // Stable sound in this area (no fade)
    public float fadeDistance = 20f;      // Fade in/out start distance
    public float maxVolume = 1.0f;
    public float minVolume = 0.0f;
    public float smoothFactor = 5f;       // How fast the volume will change

    private Vector3 lastPosition;
    private bool hasEnteredSweetSpot = false; // Keep track if the car has entered the "sweet spot"

    void Start()
    {
        engineSound.volume = minVolume; // Start with no sound
        lastPosition = transform.position;

        // Fetch the player
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }
        else
        {
            Debug.LogError("No player found for car audio source. Make sure the player has tag 'Player'.");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 carVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        bool movingTowardsPlayer = Vector3.Dot(carVelocity.normalized, directionToPlayer) > 0;

        if (distance < sweetSpotDistance)
        {
            engineSound.volume = maxVolume;
            hasEnteredSweetSpot = true;
            if (!engineSound.isPlaying)
            {
                engineSound.Play();
            }
        }
        else if (!hasEnteredSweetSpot && distance < fadeDistance && movingTowardsPlayer)
        {
            // Fade in when car is coming towards player
            float fadeFactor = 1 - ((distance - sweetSpotDistance) / (fadeDistance - sweetSpotDistance));
            engineSound.volume = Mathf.Lerp(engineSound.volume, maxVolume * fadeFactor, Time.deltaTime * smoothFactor);
            if (!engineSound.isPlaying)
            {
                engineSound.Play();
            }
        }
        else if (hasEnteredSweetSpot && distance > sweetSpotDistance)
        {
            // Fade out when car has been next to player
            float fadeFactor = (distance - sweetSpotDistance) / (fadeDistance - sweetSpotDistance);
            engineSound.volume = Mathf.Lerp(engineSound.volume, maxVolume * (1 - fadeFactor), Time.deltaTime * smoothFactor * 3);
        }

        // Stop the engine sound if the car is outside the fade distance
        if (distance > fadeDistance)
        {
            hasEnteredSweetSpot = false;
            engineSound.Stop();
        }
    }
}

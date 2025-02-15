using UnityEngine;

public class CarAudioController : MonoBehaviour
{
    public AudioSource engineSound;  // Engine sound
    private Transform player;         // Player

    public float sweetSpotDistance = 10f; // Inom denna radie är volymen konstant
    public float fadeOutDistance = 30f;   // Efter denna radie börjar ljudet fadea ut
    public float maxVolume = 1.0f;        // Maximal volym
    public float minVolume = 0.0f;        // Lägsta volym (tyst)
    public float smoothFactor = 5f;       // Hur snabbt volymen förändras

    private Rigidbody rb;
    private float targetVolume = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        engineSound.volume = minVolume; // Start with no sound

        // Fetch the player
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }
        else
        {
            Debug.LogError("No player found for audio. Make sure player has tag 'Player'!");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Volym är konstant inom "sweet spot"
        if (distance < sweetSpotDistance)
        {
            targetVolume = maxVolume;
        }
        // Börja fade ut efter sweet spot
        else if (distance < fadeOutDistance)
        {
            float fadeFactor = (distance - sweetSpotDistance) / (fadeOutDistance - sweetSpotDistance);
            targetVolume = Mathf.Lerp(maxVolume, minVolume, fadeFactor);
        }
        else
        {
            targetVolume = minVolume;
        }

        // Smidig volymjustering för att undvika hopp
        engineSound.volume = Mathf.Lerp(engineSound.volume, targetVolume, Time.deltaTime * smoothFactor);

        // Justera pitch baserat på bilens hastighet
        float speed = rb.velocity.magnitude;
        engineSound.pitch = Mathf.Clamp(0.8f + (speed / 50f), 0.8f, 1.2f);
    }
}

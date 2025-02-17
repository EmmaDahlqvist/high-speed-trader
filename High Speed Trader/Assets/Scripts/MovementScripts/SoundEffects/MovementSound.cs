using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour, PlayerMovementListener
{
    private PlayerMovement pm;

    public GameObject walking;
    public GameObject sprinting;
    public GameObject sliding;
    public GameObject crouching;
    public GameObject vaulting;

    public AudioClip landingSound;
    private AudioSource audioSource;

    private Dictionary<PlayerMovement.MovementState, GameObject> soundMap;
    private PlayerMovement.MovementState lastState; // remember last state

    void Awake()
    {
        pm = GetComponent<PlayerMovement>();

        pm.addLandingListener(this); // lägg till som landinglistener

        audioSource = gameObject.AddComponent<AudioSource>();

        // connect state to sound
        soundMap = new Dictionary<PlayerMovement.MovementState, GameObject>
        {
            { PlayerMovement.MovementState.walking, walking },
            { PlayerMovement.MovementState.sprinting, sprinting },
            { PlayerMovement.MovementState.crouching, crouching },
            { PlayerMovement.MovementState.sliding, sliding },
            { PlayerMovement.MovementState.vaulting, vaulting}
        };

        // Stäng av alla ljudobjekt från start
        foreach (var sound in soundMap.Values)
        {
            if (sound != null) sound.SetActive(false);
        }

        lastState = pm.state; // Spara initialt state
    }

    // Spela landing sound, lyssnar på playermovement när den landar
    public void PlaySound()
    {
        if(landingSound != null)
        {
            audioSource.pitch = audioSource.pitch = Random.Range(0.8f, 0.9f);
            audioSource.PlayOneShot(landingSound, 0.3f);
        }
    }

    void Update()
    {
        if (pm == null) return;



        // Kolla om state har ändrats
        if (pm.state != lastState)
        {
            UpdateSounds(pm.state);
            lastState = pm.state; // Uppdatera senaste state
        }
    }

    private void UpdateSounds(PlayerMovement.MovementState newState)
    {
        // Stäng av alla ljud först
        foreach (var sound in soundMap.Values)
        {
            if (sound != null) sound.SetActive(false);
        }

        // Aktivera bara rätt ljud om det finns i mappen
        if (soundMap.ContainsKey(newState) && soundMap[newState] != null)
        {
            soundMap[newState].SetActive(true);
        }
    }
}

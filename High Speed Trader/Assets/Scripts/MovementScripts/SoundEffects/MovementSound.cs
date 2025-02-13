using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    private PlayerMovement pm;

    public GameObject walking;
    public GameObject sprinting;
    public GameObject sliding;
    public GameObject landing;

    private Dictionary<PlayerMovement.MovementState, GameObject> soundMap;
    private PlayerMovement.MovementState lastState; // remember last state

    void Awake()
    {
        pm = GetComponent<PlayerMovement>();

        // connect state to sound
        soundMap = new Dictionary<PlayerMovement.MovementState, GameObject>
        {
            { PlayerMovement.MovementState.walking, walking },
            { PlayerMovement.MovementState.sprinting, sprinting },
            { PlayerMovement.MovementState.crouching, walking }, // Samma som walking s�l�nge
            { PlayerMovement.MovementState.sliding, sliding },
            { PlayerMovement.MovementState.landed, landing }
        };

        // St�ng av alla ljudobjekt fr�n start
        foreach (var sound in soundMap.Values)
        {
            if (sound != null) sound.SetActive(false);
        }

        lastState = pm.state; // Spara initialt state
    }

    void Update()
    {
        if (pm == null) return;



        // Kolla om state har �ndrats
        if (pm.state != lastState)
        {
            UpdateSounds(pm.state);
            lastState = pm.state; // Uppdatera senaste state
        }
    }

    private void UpdateSounds(PlayerMovement.MovementState newState)
    {
        // St�ng av alla ljud f�rst
        foreach (var sound in soundMap.Values)
        {
            if (sound != null) sound.SetActive(false);
        }

        // Aktivera bara r�tt ljud om det finns i mappen
        if (soundMap.ContainsKey(newState) && soundMap[newState] != null)
        {
            soundMap[newState].SetActive(true);
        }
    }
}

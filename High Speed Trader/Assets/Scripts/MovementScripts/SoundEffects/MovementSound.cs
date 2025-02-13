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
            { PlayerMovement.MovementState.crouching, walking }, // Samma som walking sålänge
            { PlayerMovement.MovementState.sliding, sliding },
            { PlayerMovement.MovementState.landed, landing }
        };

        // Stäng av alla ljudobjekt från start
        foreach (var sound in soundMap.Values)
        {
            if (sound != null) sound.SetActive(false);
        }

        lastState = pm.state; // Spara initialt state
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

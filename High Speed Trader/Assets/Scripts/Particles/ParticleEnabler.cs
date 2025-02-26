using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnabler : MonoBehaviour
{
    public PlayerMovement pm;
    public GameObject particleObj;
    public float requiredSprintTime = 3f; // Hur länge man måste springa
    private float sprintTimer = 0f;
    private bool isObjectActive = false;

    private AudioSource particleSound;

    private void Start()
    {
        particleObj.SetActive(false);
        particleSound = transform.GetComponent<AudioSource>();
        if(particleSound != null)
        {
            particleSound.Stop();
        }
    }

    void Update()
    {

        print(pm.state);
        // not standing still or walking
        if (pm.state != PlayerMovement.MovementState.walking && pm.state != PlayerMovement.MovementState.standingStill)
        {
            // Öka timern medan vi springer
            sprintTimer += Time.deltaTime;
            print("sprint timer " + sprintTimer);

            // Aktivera objektet om vi har sprungit tillräckligt länge
            if (sprintTimer >= requiredSprintTime && !isObjectActive)
            {
                particleObj.SetActive(true);
                isObjectActive = true;
                StartSound();
            }
        }
        else
        {
            // Nollställ timern och inaktivera objektet om vi slutar springa
            sprintTimer = 0f;
            if (isObjectActive)
            {
                particleObj.SetActive(false);
                isObjectActive = false;
                StopSound();
            }
        }
    }

    private void StartSound()
    {
        if(particleSound != null)
        {
            particleSound.volume = 0f; // Börja med volym 0
            particleSound.Play();

            StartCoroutine(FadeIn(particleSound, 1f));
        }
    }

    private void StopSound()
    {
        if(particleSound != null)
        {
            StartCoroutine(FadeOut(particleSound, 1f));
        }
    }

    // Coroutine för att fada in ljudet
    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float targetVolume = 1f; // Maxvolymen, kan justeras

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }

        audioSource.volume = targetVolume; // Se till att volymen slutar exakt på max
    }

    // Coroutine för att fada ut ljudet
    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Återställ volymen för framtida uppspelningar
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public float fadeDuration = 2.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); 
    }

    public void FadeOutAndLoadScene()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Återställ volymen ifall scenen laddas igen
    }
}


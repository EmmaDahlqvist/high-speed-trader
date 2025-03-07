using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    private Button scoreButton;
    public CanvasGroup fadeCanvasGroup;

    public void Start()
    {
        FadeIn();
        Cursor.lockState = CursorLockMode.None;
    }
    private void FadeIn()
    {
        fadeCanvasGroup.DOFade(0f, 2f) // Fadea till 0 (osynlig)
            .SetEase(Ease.InOutQuad); // S�tt easing f�r smidig �verg�ng
    }

    public async void OnButtonPress()
    {
        FadeOut();
    }
    private void FadeOut()
    {
        fadeCanvasGroup.DOFade(1f, 2f)  // Fadea till 0 (osynlig)
            .SetEase(Ease.InOutQuad).OnComplete(() => LoadLevel());                // S�tt easing f�r smidig �verg�ng
    }
    
    private async void LoadLevel()
    {
        SceneManager.LoadScene("LevelOne", LoadSceneMode.Single);
        await Task.Delay(5); // Delay for 1 millisecond
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelOne"));
    }
   
}

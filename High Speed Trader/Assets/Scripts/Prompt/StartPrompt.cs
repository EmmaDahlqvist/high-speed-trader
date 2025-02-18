using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPrompt : MonoBehaviour, TurnAroundCompleteListener
{
    public float promptTimer = 5f;
    public GameObject getReadyPromptCanvas;
    private CanvasGroup getReadyCanvasGroup;
    private CanvasGroup runCanvasGroup;
    public GameObject runPromptCanvas;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(2000, 100);
        // Fetch the canvasGroup
        getReadyCanvasGroup = getReadyPromptCanvas.GetComponent<CanvasGroup>();

        if (getReadyCanvasGroup == null)
        {
            Debug.LogError("CanvasGroup missing!");
            return;
        }

        runCanvasGroup = runPromptCanvas.GetComponent<CanvasGroup>();

        if (runPromptCanvas == null)
        {
            Debug.LogError("CanvasGroup missing!");
            return;
        }

        HideCanvasGroup(getReadyCanvasGroup);
        HideCanvasGroup(runCanvasGroup);
        getReadyCanvasGroup.interactable = false;
        getReadyCanvasGroup.blocksRaycasts = false;
        runCanvasGroup.blocksRaycasts = false;
        runCanvasGroup.blocksRaycasts = false;

        ShowPrompt(getReadyCanvasGroup, getReadyPromptCanvas);
    }

    public void ActAfterTurn()
    {
        getReadyPromptCanvas.SetActive(false);
        ShowPrompt(runCanvasGroup, runPromptCanvas);
        StartCoroutine(FadeOutRoutine(runCanvasGroup));
    }

    private void StartFadeOut(CanvasGroup canvasGroup)
    {
        StartCoroutine(FadeOutRoutine(canvasGroup));
    }

    private void ShowPrompt(CanvasGroup canvasGroup, GameObject canvas)
    {
        canvas.SetActive(true);
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutRoutine(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < promptTimer)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / promptTimer);
            yield return null;
        }

        HideCanvasGroup(canvasGroup);
    }

    private void HideCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0f;
    }
}

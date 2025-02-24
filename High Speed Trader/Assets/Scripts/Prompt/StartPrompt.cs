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

    public bool wait;

    // Start is called before the first frame update
    public void Start()
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

        if (wait)
        {
            return;
        }

        StartPrompts();
    }

    public void StartPrompts()
    {
        ShowPrompt(getReadyCanvasGroup, getReadyPromptCanvas);
    }

    public void ActAfterTurn()
    {
        HideCanvasGroup(getReadyCanvasGroup);
        ShowPrompt(runCanvasGroup, runPromptCanvas);
        runCanvasGroup.DOFade(1f, 0.5f).OnStart(() => Debug.Log("Fade started")).OnComplete(() => {
            StartCoroutine(FadeOutRoutine(runCanvasGroup));
        });
    }

    private void StartFadeOut(CanvasGroup canvasGroup)
    {
        StartCoroutine(FadeOutRoutine(canvasGroup));
    }

    private void ShowPrompt(CanvasGroup canvasGroup, GameObject canvas)
    {
        canvasGroup.DOFade(1f, 0.5f);
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

    void OnDestroy()
    {
        DOTween.KillAll();
    }
}

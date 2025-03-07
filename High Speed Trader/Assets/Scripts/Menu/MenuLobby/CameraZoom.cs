using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    public Transform zoomTarget;  // Dra in den bild/objekt du vill zooma in p�
    public float zoomDuration = 1.5f;  // Hur snabbt kameran zoomar in
    public float zoomFactor = 3f;  // Hur mycket kameran zoomar in
    public CanvasGroup fadeCanvas; // Dra in din CanvasGroup h�r
    public float fadeDuration = 1.5f;  // Hur snabbt fade-effekten sker
    public float moveCloserAmount = 5f; // Hur n�ra kameran r�r sig i Z-led

    public GameObject screenSelectorObject;
    private ScreenSelector screenSelector;

    SliderBehaviour sliderBehaviour;
    public CashManager cashManager;
    public AudioSource audioSource;

    private Camera cam;

    private void Awake()
    {
        //CASH. IF TOO LOW
        if (cashManager.GetCash() < 100)
        {
            cashManager.SetCash(100);
        }
    }

    void Start()
    {

        cam = Camera.main;

        // get the screen selector and the current lvl
        screenSelector = screenSelectorObject.GetComponent<ScreenSelector>();
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("LastLevel", screenSelector.GetCurrentLevel());
        PlayerPrefs.Save();
        CameraFollower cameraFollower = GetComponentInChildren<CameraFollower>();
        cameraFollower.StopCameraRotation();

        cam.transform.DORotate(new Vector3(0,0,0), 1, RotateMode.Fast); // rotate to look directly at pc
        Vector3 targetPosition = new Vector3(zoomTarget.position.x, zoomTarget.position.y, cam.transform.position.z - moveCloserAmount); // Flytta n�rmare i Z-led

        // Flytta kameran n�rmare + zooma in
        cam.transform.DOMove(targetPosition, zoomDuration)
            .SetEase(Ease.InOutQuad);

        cam.DOOrthoSize(cam.orthographicSize / zoomFactor, zoomDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => { StartFade(); FadeOutAndLoadScene(); }); // N�r zoom �r klar, starta fade
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
        audioSource.volume = startVolume; // �terst�ll volymen ifall scenen laddas igen
    }
    void StartFade()
    {
        GameObject currentLvlObject = screenSelector.GetCurrentLevelObject();

        Transform sliderObj = currentLvlObject.transform.Find("Slider Green");
        sliderBehaviour = sliderObj.GetComponent<SliderBehaviour>();
        cashManager = currentLvlObject.GetComponent<CashManager>();

        if (sliderBehaviour == null)
        {
            Debug.Log("null slide");
        }
        if (cashManager == null)
        {
            Debug.Log("null cash");
        }

        // Fade till svart
        print(screenSelector.GetCurrentLevel());
        fadeCanvas.DOFade(1, fadeDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => {
                cashManager.RemoveCash(sliderBehaviour.currentBet);
                Camera.main.clearFlags = CameraClearFlags.Skybox;
                SceneManager.LoadScene(screenSelector.GetCurrentLevel(),LoadSceneMode.Single);
            } ); // N�r fade �r klar, byt scen
    }

    void OnDestroy()
    {
        DOTween.KillAll();
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScreenSelector : MonoBehaviour
{

    public GameObject menuObject;
    public GameObject levelOneObject;
    public GameObject levelTwoObject;
    public GameObject levelThreeObject;
    public GameObject guideObject;
    public GameObject gameOverObject;
    public Transform screenHolderObject;
    private UIRaycastChecker UIRaycastChecker;
    public GameObject highScoreObject;
    private HighScoreScreen highScoreScreen;
    private LevelManager levelManager;
    public LockCameraZoom lockCameraZoom;
    public Toggle toggle;

    public CanvasGroup fadeCanvasGroup;

    private bool firstTimePlaying;

    private int currentLvl = 0;
    public Dictionary<int, GameObject> levelObjects = new Dictionary<int, GameObject>();

    private void Start()
    {
        highScoreScreen = highScoreObject.GetComponent<HighScoreScreen>();
        levelManager = transform.GetComponent<LevelManager>();


        if (levelManager.GetFirstTimePlaying() == true)
        {
            levelManager.SetFirstTimePlayingFalse();
            firstTimePlaying = true;
            print("first time");
        }  else
        {
            firstTimePlaying = false;
            print("not first time");

        }

        levelManager.AddCompletedLevel(0);
        levelObjects.Add(-1, gameOverObject);
        levelObjects.Add(0, menuObject);
        levelObjects.Add(1, levelOneObject);
        levelObjects.Add(2, levelTwoObject);
        levelObjects.Add(3, levelThreeObject);
   


        DeactivateAllScreens();

        UIRaycastChecker = screenHolderObject.GetComponent<UIRaycastChecker>();
        if (UIRaycastChecker == null)
        {
            Debug.LogError("screenholder did not have UIRaycastChecker script!");
        }

        // set up playbuttons, if theyre interactable or not
        for(int i = 1; i <= levelObjects.Count-1; i++) {
            SetUpPlayButtons(i);
        }

        SetScreen(levelManager.GetLastLevel());

        fadeCanvasGroup.alpha = 1f;
        FadeIn();
    }
    private void FadeIn()
    {
        fadeCanvasGroup.DOFade(0f, 2f)  // Fadea till 0 (osynlig)
            .SetEase(Ease.InOutQuad);                // S�tt easing f�r smidig �verg�ng
    }

    public int GetCurrentLevel()
    {
        return currentLvl;
    }

    private SliderBehaviour slider;

    public void OnPlayButton()
    {
        currentLvl = 1;
        if (firstTimePlaying)
        {
            guideObject.SetActive(true);
            SwitchRaycaster(guideObject);
            firstTimePlaying = false;
        } else
        {
            menuObject.SetActive(false);

            SetScreen(currentLvl);
        }
    }

    public void OnOpenGuideButton()
    {
        currentLvl = 0;
        DeactivateAllScreens();
        guideObject.SetActive(true);
        SwitchRaycaster(guideObject);
    }

    public void OnGuideOKButton()
    {
        guideObject.SetActive(false);
        SetScreen(currentLvl);
        if(currentLvl == 0)
        {
            lockCameraZoom.ZoomOut();
        }
    }

    private void SwitchRaycaster(GameObject gameObject)
    {
        if(gameObject.GetComponent<GraphicRaycaster>() == null)
        {
            print(gameObject + " s raycast is null");
        }
        UIRaycastChecker.SetRaycasterObject(gameObject.GetComponent<GraphicRaycaster>());
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnSwitchRightButton()
    {
        currentLvl += 1;
        SetScreen(currentLvl);
    }

    public void OnSwitchLeftButton()
    {
        currentLvl -= 1;
        SetScreen(currentLvl);
    }

    private void DeactivateAllScreens()
    {
        guideObject.SetActive(false);
        foreach (GameObject gameObject in levelObjects.Values)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetUpSlider()
    {
        Transform sliderObj = levelObjects[currentLvl].transform.Find("Slider Green");
        slider = sliderObj.GetComponent<SliderBehaviour>();
        slider.SetLevel(currentLvl);
        slider.SetSlider(sliderObj.GetComponent<Slider>());
        slider.Start();
    }

    private void SetUpPlayButtons(int lvl)
    {
        Transform playButtonObj = levelObjects[lvl].transform.Find ("PlayButton");
        if(playButtonObj == null )
        {
            Debug.Log("Playbutton didnt exist");
            return;
        }

        Button playButton = playButtonObj.GetComponent<Button>();
        ColorBlock colors = playButton.colors;
        // du har klarat banan innan
        if (levelManager.GetCompletedLevels().Contains(lvl - 1))
        {
            playButton.interactable = true;
        } else
        {
            playButton.interactable = false;
            TextMeshProUGUI textInButton = playButton.GetComponentInChildren<TextMeshProUGUI>();
            textInButton.text = "Finish previous!";
            colors.normalColor = Color.gray;
            playButton.colors = colors;
        }
    }


    // 0 means menu
    private void SetScreen(int level)
    {
        currentLvl = level; // set current level index

        DeactivateAllScreens();
        levelObjects[level].SetActive(true);
        SwitchRaycaster(levelObjects[level]);
        highScoreScreen.UpdateHighScore(level);
        if(level > 0)
        {
            SetUpSlider();
        }
    }

    private void StartMenu()
    {
        currentLvl = 0;
        foreach (GameObject gameObject in levelObjects.Values)
        {
            gameObject.SetActive(false);
        }
        SetScreen(0);
    }

    public void OnBackToMenuButton()
    {
        StartMenu();
    }

    public GameObject GetCurrentLevelObject()
    {
        return levelObjects[currentLvl];
    }
    

    public void onRestartGameButton()
    {
        StartMenu();
    }

    public void onTogglePressed()
    {

        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("Toggle", 1);
        } else
        {
            PlayerPrefs.SetInt("Toggle", 0);
        }
        PlayerPrefs.Save();
    }
 
}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenSelector : MonoBehaviour
{

    public GameObject menuObject;
    public GameObject levelOneObject;
    public GameObject levelTwoObject;
    public GameObject levelThreeObject;
    public Transform screenHolderObject;
    private UIRaycastChecker UIRaycastChecker;
    public GameObject highScoreObject;
    private HighScoreScreen highScoreScreen;

    private int currentLvl = 0;
    public Dictionary<int, GameObject> levelObjects = new Dictionary<int, GameObject>();
    private void Start()
    {
        highScoreScreen = highScoreObject.GetComponent<HighScoreScreen>();
        //menuObject.SetActive(true);
        levelObjects.Add(0, menuObject);
        levelObjects.Add(1, levelOneObject);
        levelObjects.Add(2, levelTwoObject);
        levelObjects.Add(3, levelThreeObject);


        foreach(GameObject gameObject in levelObjects.Values)
        {
            gameObject.SetActive(false);
        }

        UIRaycastChecker = screenHolderObject.GetComponent<UIRaycastChecker>();
        if (UIRaycastChecker == null)
        {
            Debug.LogError("screenholder did not have UIRaycastChecker script!");
        }
        SetScreen(0);

    }

    public int GetCurrentLevel()
    {
        return currentLvl;
    }

    private SliderBehaviour slider;

    public void OnPlayButton()
    {
        currentLvl = 1;
        menuObject.SetActive(false);

        levelObjects[currentLvl].SetActive(true);
        SwitchRaycaster(levelObjects[currentLvl]);

        Transform sliderObj = levelObjects[currentLvl].transform.Find("Slider Green");
        slider = sliderObj.GetComponent<SliderBehaviour>();
        slider.SetLevel(currentLvl);
        slider.Start();
    }

    private void SwitchRaycaster(GameObject gameObject)
    {
        print("gameobject " + gameObject);
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
        levelObjects[currentLvl-1].SetActive(false);
        SetScreen(currentLvl);

        Transform sliderObj = levelObjects[currentLvl].transform.Find("Slider Green");
        slider = sliderObj.GetComponent<SliderBehaviour>();
        slider.SetLevel(currentLvl);
        slider.Start();
    }

    public void OnSwitchLeftButton()
    {
        currentLvl -= 1;
        levelObjects[currentLvl+1].SetActive(false);
        SetScreen(currentLvl);

        Transform sliderObj = levelObjects[currentLvl].transform.Find("Slider Green");
        slider = sliderObj.GetComponent<SliderBehaviour>();
        slider.SetLevel(currentLvl);
        slider.Start();
    }


    // 0 means menu
    private void SetScreen(int level)
    {
        levelObjects[level].SetActive(true);
        SwitchRaycaster(levelObjects[level]);
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
    
}

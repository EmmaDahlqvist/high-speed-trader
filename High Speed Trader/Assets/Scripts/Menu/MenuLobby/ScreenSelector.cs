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

    private int currentLvl = 1;
    public Dictionary<int, GameObject> levelObjects = new Dictionary<int, GameObject>();
    private void Start()
    {
        menuObject.SetActive(true);
        levelObjects.Add(1, levelOneObject);
        levelObjects.Add(2, levelTwoObject);
        levelObjects.Add(3, levelThreeObject);


        foreach(GameObject gameObject in levelObjects.Values)
        {
            gameObject.SetActive(false);
        }

        UIRaycastChecker = screenHolderObject.GetComponent<UIRaycastChecker>();
        if(UIRaycastChecker == null)
        {
            Debug.LogError("screenholder did not have UIRaycastChecker script!");
        }
    }

    private void Update()
    {

    }

    private SliderBehaviour slider;

    public void OnPlayButton()
    {
        menuObject.SetActive(false);
        SwitchRaycaster(menuObject);


        levelObjects[currentLvl].SetActive(true);
        SwitchRaycaster(levelObjects[currentLvl]);

        Transform sliderObj = levelObjects[currentLvl].transform.Find("Slider Green");
        slider = sliderObj.GetComponent<SliderBehaviour>();
        slider.SetLevel(currentLvl);
        slider.Start();
    }

    private void SwitchRaycaster(GameObject gameObject)
    {
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
        levelObjects[currentLvl].SetActive(true);
        SwitchRaycaster(levelObjects[currentLvl]);;

        Transform sliderObj = levelObjects[currentLvl].transform.Find("Slider Green");
        slider = sliderObj.GetComponent<SliderBehaviour>();
        slider.SetLevel(currentLvl);
        slider.Start();
    }

    public void OnSwitchLeftButton()
    {
        currentLvl -= 1;
        levelObjects[currentLvl+1].SetActive(false);
        levelObjects[currentLvl].SetActive(true);
        SwitchRaycaster(levelObjects[currentLvl]); ;

        Transform sliderObj = levelObjects[currentLvl].transform.Find("Slider Green");
        slider = sliderObj.GetComponent<SliderBehaviour>();
        slider.SetLevel(currentLvl);
        slider.Start();
    }

    public void OnBackToMenuButton()
    {
        print("back to menu");
        currentLvl = 1;
        foreach (GameObject gameObject in levelObjects.Values)
        {
            gameObject.SetActive(false);
        }
        menuObject.SetActive(true);
        SwitchRaycaster(menuObject);
    }

    public GameObject GetCurrentLevelObject()
    {
        return levelObjects[currentLvl];
    }
    
}

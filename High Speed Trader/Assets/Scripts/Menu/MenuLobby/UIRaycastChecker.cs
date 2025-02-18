using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIRaycastChecker : MonoBehaviour
{
    public Camera playerCamera;  // Tilldela din huvudkamera h�r
    public GraphicRaycaster raycaster; // Tilldela din UI-canvas GraphicRaycaster

    private List<UIHitListener> uiHitListeners = new List<UIHitListener>();

    private void Start()
    {
        if(transform.GetComponent<CameraFollower>() != null)
        {
            uiHitListeners.Add(transform.GetComponent<CameraFollower>());
        }
    }

    void Update()
    {
        if (IsLookingAtUI())
        {
            foreach(UIHitListener uiHitListener in uiHitListeners)
            {
                uiHitListener.ActOnHit();
            }
        } else
        {
            foreach (UIHitListener uiHitListener in uiHitListeners)
            {
                uiHitListener.NoLongerHit();
            }
        }
    }

    bool IsLookingAtUI()
    {
        // pointer
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = new Vector2(Screen.width / 2, Screen.height / 2);

        // hit objects
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        return results.Count > 0; // found UI object
    }

    public void SetRaycasterObject(GraphicRaycaster raycaster)
    {
        this.raycaster = raycaster;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        print("opening laptop");
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] public LayerMask _interactableMask;
     public InteractionPromptStationary _goalCanvas;
 
    // how many colliders we will search for
    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
        
        if(_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            
            if(_interactable != null)
            {
                if(!_goalCanvas.isDisplayed)
                {
                    _goalCanvas.SetUp(_interactable.InteractionPrompt);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _interactable.Interact(this);
                }
            }
        } else
        {
            if(_interactable != null) _interactable = null;
            if (_goalCanvas.isDisplayed) _goalCanvas.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}

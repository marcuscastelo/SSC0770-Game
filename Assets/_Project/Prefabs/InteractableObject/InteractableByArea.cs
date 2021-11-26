using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Core;

public class InteractableByArea : MonoBehaviour, IInteractable
{
    // public InteractableObject parentSO;

    // void OnTriggerEnter2D(Collider2D collider2D)
    // {
    //     Debug.Log("InteractableByArea - OnTriggerEnter2D: " + collider2D.name);
    //     parentSO.SetSelected(true);
    //     if (collider2D.GetComponentInParent<Player>() != null)
    //     {
    //     }
    // }

    // void OnTriggerExit2D(Collider2D collider2D)
    // {
    //     Debug.Log("InteractableByArea - OnTriggerExit2D: " + collider2D.name);
    //     parentSO.SetSelected(false);
    //     if (collider2D.GetComponentInParent<Player>() != null)
    //     {
    //     }
    // }

    private InteractableObject _parentSO;

    void Start()
    {
        _parentSO = GetComponentInParent<InteractableObject>();
        Debug.Assert(_parentSO != null, "InteractableByArea - Start: _parentSO is null");
    }
    
    public void OnSelected() => _parentSO.OnSelected();
    public void OnDeselected() => _parentSO.OnDeselected();
    public void OnInteract(Interaction interaction) => _parentSO.OnInteract(interaction);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Core;

[ExecuteAlways]
public class InteractableByArea : MonoBehaviour, IInteractable
{   
    [SerializeField] [ReadOnly] private InteractableObject _parentSO;
    void Start()
    {
        _parentSO = GetComponentInParent<InteractableObject>();
        Debug.Assert(_parentSO != null, "InteractableByArea - Start: _parentSO is null");
    }
    
    public void OnSelected() => _parentSO.OnSelected();
    public void OnDeselected() => _parentSO.OnDeselected();
    public void OnInteract(Interaction interaction) => _parentSO.OnInteract(interaction);
}

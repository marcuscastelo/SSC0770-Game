using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Hypnos.Core;

[ExecuteInEditMode]
public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] [ReadOnly] private SpriteRenderer spriteRenderer;
    [SerializeField] [ReadOnly] private Sprite spriteWhenUnselected;
    [SerializeField] [ReadOnly] private Sprite spriteWhenSelected;

    [Header("Config")]
    public string id;

    [Header("Events")]
    
    //TODO: stop using UnityEvent, because this object cares if someone receives it (https://github.com/modesttree/Zenject/blob/master/Documentation/Signals.md#when-to-use-signals)
    [SerializeField] private UnityEvent<Interaction> onInteracted;

    private bool _selected;

    public void SetSelected(bool selected)
    {
        if (_selected == selected) return;

        _selected = selected;
        _UpdateSprite();
    }

    private void _UpdateSprite()
    {
        spriteRenderer.sprite = _selected ? spriteWhenSelected : spriteWhenUnselected;
    }

    void Start()
    {
        _UpdateSprite();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            _UpdateSprite();
        }
    }

    public void OnSelected()
    {
        Debug.Log("Selected");
        SetSelected(true);
    }

    public void OnDeselected()
    {
        Debug.Log("Deselected");
        SetSelected(false);
    }

    public void OnInteract(Interaction interaction)
    {
        Debug.Log("Interacted by player (id=" + id + ")");
        interaction.StartInteraction();
        if (onInteracted != null)
        {
            onInteracted.Invoke(interaction);
        }
        else
        {
            interaction.EndInteraction(false);
        }
    }
}

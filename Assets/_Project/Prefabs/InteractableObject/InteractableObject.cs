using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Hypnos.Core;
using Zenject;

[ExecuteInEditMode]
public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteWhenUnselected;
    [SerializeField] private Sprite spriteWhenSelected;

    [Header("Config")]
    public string id;

    private IInteractionResponse _interactionResponse;

    void Awake()
    {
        _interactionResponse = GetComponent<IInteractionResponse>();
    }

    private bool _selected;

    public void SetSelected(bool selected)
    {
        if (_selected == selected) return;

        _selected = selected;
        _UpdateSprite();
    }

    private void _UpdateSprite() //TODO: ISelectionResponse
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
        SetSelected(true);
    }

    public void OnDeselected()
    {
        SetSelected(false);
    }

    public void OnInteract(Interaction interaction)
    {
        interaction.StartInteraction();
        if (_interactionResponse != null)
            _interactionResponse.OnInteracted(interaction);
        else
        {
            Debug.LogWarning("No IInteractionResponse found on " + gameObject.name);
            interaction.EndInteraction(false);
        }
    }
}

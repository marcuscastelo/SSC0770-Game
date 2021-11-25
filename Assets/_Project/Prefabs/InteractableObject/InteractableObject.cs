using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteWhenUnselected;
    public Sprite spriteWhenSelected;

    [Header("Config")]

    public string id;

    [Header("Events")]

    public UnityEvent onSelected;
    public UnityEvent onUnselected;
    public UnityEvent<IInteractor> onInteracted;

    private bool _selected;

    public void SetSelected(bool selected)
    {
        if (_selected == selected) return;

        _selected = selected;
        _UpdateSprite();

        if (selected)
            onSelected.Invoke();
        else
            onUnselected.Invoke();
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
        onSelected?.Invoke();
        SetSelected(true);
    }

    public void OnDeselected()
    {
        Debug.Log("Deselected");
        onUnselected?.Invoke();
        SetSelected(false);
    }

    public void OnInteract(IInteractor interactor)
    {
        Debug.Log("Interacted by player (id=" + id + ")");
        onInteracted?.Invoke(interactor);
    }
}

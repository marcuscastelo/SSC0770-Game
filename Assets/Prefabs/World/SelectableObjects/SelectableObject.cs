using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class SelectableObject : MonoBehaviour
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
    public UnityEvent onInteracted;

    private bool isSelected;

    public void SetSelected(bool selected)
    {
        if (isSelected == selected) return;

        isSelected = selected;
        _UpdateSprite();

        if (selected)
            onSelected.Invoke();
        else
            onUnselected.Invoke();
    }

    private void _UpdateSprite()
    {
        spriteRenderer.sprite = isSelected ? spriteWhenSelected : spriteWhenUnselected;
    }

    // Start is called before the first frame update
    void Start()
    {
        _UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            _UpdateSprite();
        }
    }
}

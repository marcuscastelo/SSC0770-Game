using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SelectableObject : MonoBehaviour
{
    public string id;
    public SpriteRenderer spriteRenderer;

    public Sprite spriteWhenUnselected;
    public Sprite spriteWhenSelected;

    private bool isSelected;

    public void SetSelected(bool selected)
    {
        if (isSelected == selected) return;

        isSelected = selected;
        _UpdateSprite();
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

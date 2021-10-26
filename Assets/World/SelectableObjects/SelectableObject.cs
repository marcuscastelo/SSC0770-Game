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

    void SetSelected(bool selected)
    {
        isSelected = selected;
        _UpdateSprite();
    
        if (!selected) {
            if (GameState.Instance.selectedObject == this) {
                GameState.Instance.selectedObject = null;
            }
        }

        else {
            if (GameState.Instance.selectedObject != null && GameState.Instance.selectedObject != this) {
                GameState.Instance.selectedObject.SetSelected(false);
            }
            GameState.Instance.selectedObject = this;
        }

    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            Debug.Log("Collision Enter detected");
            if (!isSelected)
            {
                SetSelected(true);
                _UpdateSprite();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            Debug.Log("Collision Exit detected");
            SetSelected(false);
            _UpdateSprite();
        }
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

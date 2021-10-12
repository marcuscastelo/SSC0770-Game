using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SelectableObject : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite spriteWhenUnselected;
    public Sprite spriteWhenSelected;

    [SerializeField]
    private bool isSelected;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            Debug.Log("Collision Enter detected");
            isSelected = true;
            _UpdateSprite();
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            Debug.Log("Collision Exit detected");
            isSelected = false;
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

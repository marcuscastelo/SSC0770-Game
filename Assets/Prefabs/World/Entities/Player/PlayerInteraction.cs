using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    public Player player;

    [HideInInspector]
    private SelectableObject selectedObject;

    public SelectableObject SelectedObject { get { return selectedObject; } }

    void Awake()
    {
        if (player == null)
            player = transform.parent.GetComponent<Player>();
    }

    public void Interact()
    {
        if (selectedObject != null)
        {
            Debug.Log("Interacting with " + selectedObject.id);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PLAYER: Collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "SelectableObject")
        {
            GameObject parentGameObject = collision.gameObject.transform.parent.gameObject;
            Debug.Log("PLAYER: Object is tagged SelectableObject");
            SelectableObject selectableObj = parentGameObject.GetComponent<SelectableObject>();
            if (selectableObj != null) {
                Debug.Log("PLAYER: Object has SelectableObject component");
                this.selectedObject = selectableObj;
            } else {
                Debug.LogError("PLAYER: Object has no SelectableObject component");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("PLAYER: Exit collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "SelectableObject")
        {
            GameObject parentGameObject = collision.gameObject.transform.parent.gameObject;
            Debug.Log("PLAYER: Object is tagged SelectableObject");
            SelectableObject selectableObj = parentGameObject.GetComponent<SelectableObject>();
            if (selectableObj != null)
            {
                Debug.Log("PLAYER: Object has SelectableObject component");
                if (selectableObj == selectedObject)
                {
                    Debug.Log("PLAYER: Object is selected object, deselecting");
                    this.selectedObject = null;
                }
            }
        }
    }
}

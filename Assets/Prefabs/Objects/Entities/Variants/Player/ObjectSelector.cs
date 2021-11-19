using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSelector : MonoBehaviour
{
    [Header("References")]
    public Player player;

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
                this.player.SelectedObject = selectableObj;
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
                if (selectableObj == player.SelectedObject)
                {
                    Debug.Log("PLAYER: Object is selected object, deselecting");
                    this.player.SelectedObject = null;
                }
            }
        }
    }
}

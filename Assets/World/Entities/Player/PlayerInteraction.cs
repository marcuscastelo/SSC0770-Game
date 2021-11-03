using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Player player;

    [HideInInspector]
    public SelectableObject selectedObject;

    private const UnityEngine.KeyCode INTERACT_KEY = KeyCode.E;

    public void OnObjectSelected(SelectableObject obj)
    {
        selectedObject = obj;
    }

    public void OnObjectUnselected()
    {
        selectedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(INTERACT_KEY))
        {
            if (selectedObject != null)
            {
                if (player.ActiveBuff == Buff.NoItem)
                    player.ActiveBuff = Buff.Armor;
                else
                    player.ActiveBuff = Buff.NoItem;                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PLAYER: Collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "SelectableObject")
        {
            SelectableObject selObj = collision.gameObject.GetComponent<SelectableObject>();
            if (selObj != null)
                OnObjectSelected(selObj);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("PLAYER: Exit collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "SelectableObject")
        {
            SelectableObject selObj = collision.gameObject.GetComponent<SelectableObject>();
            if (selObj != null)
                OnObjectUnselected();
        }
    }
}

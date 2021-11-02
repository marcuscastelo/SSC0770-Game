using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
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
}

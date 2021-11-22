using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObjectInteraction : MonoBehaviour
{
    public SelectableObject parentSO;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Debug.Log("OnTriggerEnter2D");
        // Debug.Log(collider2D.gameObject.name);
        // Debug.Log(collider2D.gameObject.tag);
        if (collider2D.gameObject.tag == "Player")
        {
            parentSO.SetSelected(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        // Debug.Log("OnTriggerExit2D");
        // Debug.Log(collider2D.gameObject.name);
        // Debug.Log(collider2D.gameObject.tag);
        if (collider2D.gameObject.tag == "Player")
        {
            parentSO.SetSelected(false);
        }
    }
}

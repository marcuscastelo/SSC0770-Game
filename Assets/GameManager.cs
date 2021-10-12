using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EntityState playerState;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    void OnInteract(SelectableObject obj)
    {
        string id = obj.id;
        Debug.Log("Interacted with " + id);
    }

    void FixedUpdate()
    {
        if (playerState.wantsToInteract) 
        {
            if (GameState.Instance.selectedObject != null) {
                OnInteract(GameState.Instance.selectedObject);
                playerState.wantsToInteract = false;
            }
        }
    }
}

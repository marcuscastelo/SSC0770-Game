using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public PlayerData playerData;

    private bool __hack_interacted = false;
    private bool __hack_ok_pressed = false;

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
        __hack_ok_pressed = false;
        __hack_interacted = true;
    }

    void FixedUpdate()
    {
        if (GameState.Instance.selectedObject == null) 
        {
            __hack_interacted = false;
        }

        // // if (playerData.wantsToInteract) 
        // {
        //     if (GameState.Instance.selectedObject != null) {
        //         OnInteract(GameState.Instance.selectedObject);
        //         // playerData.wantsToInteract = false;
        //     }
        // }
    }

    void OnGUI()
    {
        GUI.skin.window.fixedHeight = 300;
        if (__hack_interacted && !__hack_ok_pressed)
            GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75, 800, 300), ShowGUI, "");
    }

    void ShowGUI(int windowID)
    {
        GUI.skin.label.fontSize = 40;
        GUI.skin.button.fontSize = 40;

        GUI.Label(new Rect(25, 25, 1000, 100), "Deseja mesmo selecionar " + GameState.Instance.selectedObject.id + "?");
        if (GUI.Button(new Rect(25, 125, 200, 100), "OK"))
        {
            __hack_ok_pressed = true;
            __hack_interacted = false;
        }
    }
}

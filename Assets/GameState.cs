using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState instance;
    public static GameState Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameState>();
            }
            return instance;
        }
    }

    public SelectableObject selectedObject;
}

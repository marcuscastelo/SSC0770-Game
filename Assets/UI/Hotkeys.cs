using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hotkeys : MonoBehaviour
{
    public KeyCode pauseHotkey = KeyCode.P;
    public UnityEvent onPause;

    void Update()
    {
        if (Input.GetKeyDown(pauseHotkey))
        {
            onPause.Invoke();
        }
    }
}
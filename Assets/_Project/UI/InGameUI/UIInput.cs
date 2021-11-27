using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    [Header("References")]
    public UILayer hudUILayer;
    public PauseMenu pauseUILayer;

    UIControls uiControls;

    void OnEnable()
    {
        uiControls = new UIControls();
        SetCallbacks();
        uiControls.UI.Enable();
    }

    void OnDisable()
    {
        uiControls.UI.Disable();
        uiControls = null;
    }

    void SetCallbacks()
    {
        uiControls.UI.Pause.canceled += ctx =>
        {
            pauseUILayer.TogglePause();
        };
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    [Header("References")]
    public UILayer hudUILayer;
    public PauseMenu pauseUILayer;

    UIControls uiControls;

    void Awake()
    {
        uiControls = new UIControls();
    }
    
    void OnEnable()
    {
        uiControls.UI.Enable();
    }

    void OnDisable()
    {
        uiControls.UI.Disable();
    }

    void Start()
    {
        uiControls.UI.Pause.canceled += ctx =>
        {
            pauseUILayer.TogglePause();
        };
    }

}

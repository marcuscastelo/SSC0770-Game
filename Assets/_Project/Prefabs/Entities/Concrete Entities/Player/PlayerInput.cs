using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Entities;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    public EntityController controller;
    
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable() 
    {
        if (controls == null)
            controls = new PlayerControls();
        controls.Default.Enable();
    }

    void OnDisable() 
    {
        controls.Default.Disable();
    }

    void Start()
    {
        controls.Default.Walk.performed += ctx => controller.Move(ctx.ReadValue<Vector2>());
        controls.Default.Walk.canceled += ctx => controller.Move(Vector2.zero);

        controls.Default.Interact.started += ctx => controller.Interact();
        controls.Default.Attack.started += ctx => controller.Attack();
        controls.Default.Dash.started += ctx => controller.Dash();
    }
}

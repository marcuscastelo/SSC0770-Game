using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Hypnos.Entities;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    private EntityController _controller;
    private PlayerControls _controls;

    [Inject]
    public void Construct(EntityController controller)
    {
        _controller = controller;
    }

    void OnEnable()
    {
        _controls = new PlayerControls();
        SetCallbacks();
        _controls.Enable();
    }

    void OnDisable() 
    {
        _controls.Disable();
    }

    void SetCallbacks()
    {
        _controls.Default.Walk.started += ctx => _controller.Move(ctx.ReadValue<Vector2>());
        _controls.Default.Walk.performed += ctx => _controller.Move(ctx.ReadValue<Vector2>());
        _controls.Default.Walk.canceled += ctx => _controller.Move(Vector2.zero);

        _controls.Default.Interact.started += ctx => _controller.Interact();
        _controls.Default.Attack.started += ctx => _controller.Attack();
        _controls.Default.Dash.started += ctx => _controller.Dash();

        _controls.Debug.Restart.started += ctx => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

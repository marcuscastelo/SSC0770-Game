using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    public Player player;
    
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
        //TODO: maybe rename Default to Player -> so that PlayerBrain can inherit from IPlayerActions and implement the interface
        //Currently it would be confusing for PlayerBrain to implement IDefaultActions
        controls.Default.Walk.performed += ctx => player.brain.OnWalkInput(ctx.ReadValue<Vector2>().normalized);
        controls.Default.Walk.canceled += ctx => player.brain.OnWalkInput(Vector2.zero);

        controls.Default.Interact.started += ctx => player.brain.OnInteractInput();
        controls.Default.Attack.started += ctx => player.brain.OnAttackInput();
        controls.Default.Dash.started += ctx => player.brain.OnDashInput();
    }

    void FixedUpdate()
    {
        //After hotswapping, the player will be stuck (player controls do not work anymore)
        //So we need to re-enable the controls
        // controls.Default.Enable();
        //PS: it did not work at all.
    }
}

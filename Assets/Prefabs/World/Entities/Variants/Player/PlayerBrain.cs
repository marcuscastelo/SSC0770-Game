using UnityEngine;

// This class is used to process input received from the player.
// It is used to decide what actions to take, and then send the actions to the player controller.
public class PlayerBrain : MonoBehaviour
{
    public Player player;

    [SerializeField]
    private PlayerController controller;

    public PlayerBrainState CurrentState { get; private set; }

    void Start()
    {
        PlayerBrainState initialState = new PBWalkState(player);
        CurrentState = initialState;
        if (CurrentState == null) {
            Debug.LogError("CurrentState is null");
        }
    }

    public void OnWalkInput(Vector2 inputVector)
    {
        if (CurrentState.CanWalk()) {
            controller.Move(inputVector);
        }
    }
    
    public void OnDashInput()
    {
        if (CurrentState.CanDash())
            controller.Dash();
    }

    public void OnAttackInput()
    {
        if (CurrentState.CanAttack())
            controller.Attack();
    }

    public void OnInteractInput()
    {
        if (CurrentState.CanInteract())
            controller.Interact();
    }
}
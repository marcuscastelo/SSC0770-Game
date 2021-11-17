using UnityEngine;

// This class is used to process input received from the player.
// It is used to decide what actions to take, and then send the actions to the player controller.
public sealed class PlayerBrain : MonoBehaviour
{
    [Header("References")]
    public Player player;
    public PlayerController controller;

    private PBStateMachine stateMachine;
    private PlayerBrainState CurrentState { get { return stateMachine.CurrentState; } }

    void Start()
    {
        stateMachine = new PBStateMachine(this);
        stateMachine.ChangeState(stateMachine.CreateInitialState);
    }

    void FixedUpdate()
    {
        stateMachine.Tick(Time.fixedDeltaTime);
    }

    public void OnWalkInput(Vector2 inputVector)
    {
        if (CurrentState.CanWalk()) {
            controller.Move(inputVector);
            bool wantsToWalk = inputVector.magnitude > 0.1f;
            if (wantsToWalk) {
                stateMachine.ChangeState(() => new PBWalkState(this));
            }
            else {
                stateMachine.ChangeState(() => new PBIdleState(this));
            }
        }
    }
    
    public void OnDashInput()
    {
        if (CurrentState.CanDash()) {
            stateMachine.ChangeState(() => {
                var ds = new PBDashState(this);
                ds.OnEntered += () => {
                    controller.Dash();
                };
                ds.OnExited += () => {
                    stateMachine.ChangeState(() => new PBIdleState(this));
                };
                return ds;
            });
        }
    }

    public void OnAttackInput()
    {
        if (CurrentState.CanAttack()) {
            controller.Attack();
            stateMachine.ChangeState(() => new PBAttackState(this));
        }
    }

    public void OnInteractInput()
    {
        if (CurrentState.CanInteract()) {
            controller.Interact();
            stateMachine.ChangeState(() => new PBInteractState(this));
        }
    }
}
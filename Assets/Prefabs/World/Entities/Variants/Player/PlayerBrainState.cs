public abstract class PlayerBrainState : IState
{
    protected Player player;
    public PlayerBrainState(Player player)
    {
        this.player = player;
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    public abstract bool CanWalk();
    public abstract bool CanDash();
    public abstract bool CanAttack();
    public abstract bool CanInteract();
}
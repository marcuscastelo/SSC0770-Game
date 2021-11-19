using System;

public abstract class PlayerBrainState : IState
{
    protected PlayerBrain brain;
    public PlayerBrainState(PlayerBrain brain)
    {
        this.brain = brain;
    }

    public abstract bool CanWalk();
    public abstract bool CanDash();
    public abstract bool CanAttack();
    public abstract bool CanInteract();
}
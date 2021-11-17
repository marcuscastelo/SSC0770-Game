public class PBDashState : PlayerBrainState
{
    public PBDashState(PlayerBrain brain) : base(brain) { }

    protected override void EnterBehaviour() { }
    protected override void ExecuteBehaviour(float deltaTime)
    {
        if (brain.controller.CurrentVelocity.magnitude < 0.1f) 
            Exit();
    }
    protected override void ExitBehaviour() { }

    public override bool CanWalk() { return false; }
    public override bool CanDash() { return false; }
    public override bool CanAttack() { return false; }
    public override bool CanInteract() { return false; }
}
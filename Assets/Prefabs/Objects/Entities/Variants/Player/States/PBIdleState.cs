public class PBIdleState : PlayerBrainState
{
    public PBIdleState(PlayerBrain brain) : base(brain) { }

    protected override void EnterBehaviour() { }
    protected override void ExecuteBehaviour(float deltaTime) { }
    protected override void ExitBehaviour() { }

    public override bool CanWalk() { return true; }
    public override bool CanDash() { return true; }
    public override bool CanAttack() { return true; }
    public override bool CanInteract() { return true; }
}
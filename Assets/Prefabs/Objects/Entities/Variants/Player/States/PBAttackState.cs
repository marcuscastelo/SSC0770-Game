public class PBAttackState : PlayerBrainState
{
    public PBAttackState(PlayerBrain brain) : base(brain) { }

    protected override void EnterBehaviour() { }
    protected override void ExecuteBehaviour(float deltaTime) { }
    protected override void ExitBehaviour() { }


    public override bool CanWalk() { return false; }
    public override bool CanDash() { return false; }
    public override bool CanAttack() { return false; }
    public override bool CanInteract() { return false; }
}
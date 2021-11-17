public class PBInteractState : PlayerBrainState
{
    public PBInteractState(Player player) : base(player) { }

    public override void Enter() { }
    public override void Execute() { }
    public override void Exit() { }

    public override bool CanWalk() { return false; }
    public override bool CanDash() { return false; }
    public override bool CanAttack() { return false; }
    public override bool CanInteract() { return false; }
}
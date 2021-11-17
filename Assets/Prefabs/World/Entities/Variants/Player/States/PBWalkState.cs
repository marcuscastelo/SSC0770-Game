public class PBWalkState : PlayerBrainState
{
    public PBWalkState(Player player) : base(player) { }

    public override void Enter() { }
    public override void Execute() { }
    public override void Exit() { }

    public override bool CanWalk() { return true; }
    public override bool CanDash() { return true; }
    public override bool CanAttack() { return true; }
    public override bool CanInteract() { return true; }
}
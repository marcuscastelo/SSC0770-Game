namespace Hypnos.Core
{
    public interface IAttackable
    {
        void SetInvulnerable(bool invulnerable);
        void OnHurt(IAttacker attacker, int damage);
    }
}
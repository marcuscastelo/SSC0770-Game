namespace Hypnos.Core
{
    public interface IAttackable
    {
        void OnHurt(IAttacker attacker);
    }
}
using UnityEngine;

public class EntityCombat: MonoBehaviour, IAttackable, IAttacker
{
    [SerializeField] private CombatStats combatStats;
    public CombatStats Stats => combatStats;

    public void Attack()
    {
        Debug.Log("EntityCombat.Attack()");
    }

    public void OnAttacked(IAttacker attacker)
    {
        Debug.Log("EntityCombat.OnAttacked()");
    }
}
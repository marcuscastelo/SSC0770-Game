using UnityEngine;
using System.Collections.Generic;


public class EntityCombat: MonoBehaviour, IAttackable, IAttacker
{
    [SerializeField] private CombatStats combatStats;
    [SerializeField] private List<AttackerArea> attackerAreas;
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

internal struct AttackerArea
{
    public Collider2D triggerArea;
    public bool alwaysActive = false;
}
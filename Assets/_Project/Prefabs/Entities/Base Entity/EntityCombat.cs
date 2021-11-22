using UnityEngine;

public class EntityCombat: MonoBehaviour, IAttackable, IAttacker
{
    public void Attack()
    {
        Debug.Log("EntityCombat.Attack()");
    }

    public void OnAttacked(IAttacker attacker)
    {
        Debug.Log("EntityCombat.OnAttacked()");
    }
}
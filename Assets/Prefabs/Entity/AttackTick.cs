using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTick : MonoBehaviour
{

    public EntityState state;
    public Collider2D attackCollider;

    private float __hack_attack_tick_count = 0;

    public bool CanAttack()
    {
        //In the future, jump will block attacking
        return !state.IsAttacking && !state.attackAnimTriggerPending;
    }

    void FixedUpdate()
    {
        if (CanAttack() && state.wantsToAttack)
        {
            state.currentVelocity = Vector2.zero;
            state.attackAnimTriggerPending = true;
        }
        state.wantsToAttack = false;

        if (state.IsAttacking)
        {
            __hack_attack_tick_count += Time.fixedDeltaTime;
            if (__hack_attack_tick_count >= 0.2f)
            {
                attackCollider.enabled = true;
                var oldOffet = attackCollider.offset;
                attackCollider.offset = oldOffet + new Vector2(0, -0.1f);
                attackCollider.offset = oldOffet;
            }
        }
        else
        {
            __hack_attack_tick_count = 0;
            attackCollider.enabled = false;
        }
    }
}
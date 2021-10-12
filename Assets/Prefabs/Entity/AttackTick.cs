using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTick : MonoBehaviour
{
    public EntityState state;

    public bool CanAttack() {
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
    }
}
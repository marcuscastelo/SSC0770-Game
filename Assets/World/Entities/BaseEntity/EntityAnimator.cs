using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    public EntityState state;
    public Animator animator;
    public Transform transformToFlip;

    private void FixedUpdate() {
        animator.SetFloat("speedX", state.currentVelocity.x);
        animator.SetFloat("speedY", state.currentVelocity.y);
        if (state.attackAnimTriggerPending) {
            animator.SetTrigger("attackTrigger");
            state.attackAnimTriggerPending = false;
        }
        if (state.currentVelocity.x < 0) {
            transformToFlip.localScale = new Vector3(-1, 1, 1);
        } else if (state.currentVelocity.x > 0) {
            transformToFlip.localScale = new Vector3(1, 1, 1);
        }
    }
}

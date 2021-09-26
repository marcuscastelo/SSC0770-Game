using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    public Movement movement;
    public Animator animator;
    public Transform transformToFlip;

    private void FixedUpdate() {
        animator.SetFloat("speedX", movement.Velocity.x);
        animator.SetFloat("speedY", movement.Velocity.y);
        if (movement.Velocity.x < 0) {
            transformToFlip.localScale = new Vector3(-1, 1, 1);
        } else if (movement.Velocity.x > 0) {
            transformToFlip.localScale = new Vector3(1, 1, 1);
        }
    }
}

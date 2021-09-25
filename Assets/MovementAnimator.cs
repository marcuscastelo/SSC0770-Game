using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    public Movement movement;
    public Animator animator;

    private void FixedUpdate() {
        animator.SetFloat("speedX", movement.Velocity.x);
        animator.SetFloat("speedY", movement.Velocity.y);
    }
}

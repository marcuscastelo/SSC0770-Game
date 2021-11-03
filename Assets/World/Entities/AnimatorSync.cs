using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSync : MonoBehaviour
{
    public Animator animator;
    public EntityMovement movement;

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speedX", movement.CurrentVelocity.x);
        animator.SetFloat("speedY", movement.CurrentVelocity.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState : MonoBehaviour
{
    public Animator animator;

    // User input indicators
    public Vector2 movementWill = Vector2.zero;
    public bool wantsToAttack = false;
    public bool wantsToInteract = false;
    
    // Actual entity stats
    public Vector2 currentVelocity = Vector2.zero;
    public bool attackAnimTriggerPending = false;

    // Animation Controller Gets
    public bool IsAttacking { get { return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"); } }
}
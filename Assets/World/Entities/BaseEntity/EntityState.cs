using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState : MonoBehaviour
{

    [Range(0, 5)]
    public int maxHealth = 1;
    public Animator animator;

    // User input indicators
    public Vector2 movementWill = Vector2.zero;
    public bool wantsToAttack = false;
    public bool wantsToInteract = false;
    
    // Actual entity stats
    public Vector2 currentVelocity = Vector2.zero;
    public bool attackAnimTriggerPending = false;

    private int _currentHealth;
    public int CurrentHealth {
        get {
            return _currentHealth;
        }
        set {
            _currentHealth = value;
            if (_currentHealth <= 0) {
                _currentHealth = 0;
            }
        }
    }

    // Animation Controller Gets
    public bool IsAttacking { get { return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"); } }

    void Start()
    {
        _currentHealth = maxHealth;
    }
}
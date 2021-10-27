using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRTI
{
    // Runtime information
    public Vector2 currentVelocity = Vector2.zero;
    public bool attackAnimTriggerPending = false;

    private int _currentHealth = -1;
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
}
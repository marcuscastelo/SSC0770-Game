using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTick : MonoBehaviour
{
    public Rigidbody2D rb;
    public float acceleration = 1;
    public float maxSpeed = 20;
    public float friction = 0.9f;

    public EntityState state;

    protected virtual bool CanMove() {
        return !state.IsAttacking;
    }
    
    void FixedUpdate() { 
        // Currently used for attack state / animation (stops player immediately)   
        if (!CanMove()) {
            state.currentVelocity = Vector2.zero;
            return;
        }

        // Apply acceleration
        state.currentVelocity += state.movementWill * acceleration * Time.fixedDeltaTime;

        // Apply friction
        state.currentVelocity *= friction;

        // Limit speed
        if (state.currentVelocity.magnitude > maxSpeed) {
            state.currentVelocity = state.currentVelocity.normalized * maxSpeed;
        }

        // Apply velocity
        rb.MovePosition(rb.position + state.currentVelocity * Time.fixedDeltaTime);
    }
}

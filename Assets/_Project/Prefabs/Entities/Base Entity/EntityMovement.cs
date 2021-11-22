using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour, IMoveable
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MovementStats movementStats;

    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 InputDirection { get; private set; }
    public Vector2 LastLookDirection { get; private set; }

    private void Awake()
    {
        if (movementStats == null)
            movementStats = new MovementStats();

        Debug.Assert(rb != null, "EntityMovement.Awake() - rb is null");
        Debug.Assert(movementStats != null, "EntityMovement.Awake() - movementStats is null");
    }

    public void Move(Vector2 direction)
    {
        InputDirection = direction;
        CurrentVelocity = direction * movementStats.maxSpeed;
        LastLookDirection = direction;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + CurrentVelocity * Time.fixedDeltaTime);
    }

    // private void ApplyAcceleration(float deltaTime, bool compensateDeceleration = true)
    // {
    //     CurrentVelocity += InputVector * (Acceleration + Deceleration * (compensateDeceleration ? 1f : 0f)) * deltaTime;
    //     CurrentVelocity = Vector2.ClampMagnitude(CurrentVelocity, MaxSpeed);
    // }

    // private void ApplyDeceleration(float deltaTime)
    // {
    //     if (CurrentVelocity.sqrMagnitude > 0.01f)
    //         CurrentVelocity -= Vector2.ClampMagnitude(CurrentVelocity.normalized * Deceleration * deltaTime, CurrentVelocity.magnitude);
    // }


}

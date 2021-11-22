using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour, IMoveable
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private MovementStats movementStats;
    public MovementStats Stats => movementStats;

    public Vector2 Position { get { return rb.position; } }
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 _targetVelocity;
    private float _acceleration;

    private void Awake()
    {
        if (movementStats == null)
            movementStats = new MovementStats();

        Debug.Assert(rb != null, "EntityMovement.Awake() - rb is null");
        Debug.Assert(movementStats != null, "EntityMovement.Awake() - movementStats is null");
    }

    public void Teleport(Vector2 position)
    {
        rb.position = position;
    }

    public void SetVel(Vector2 velocity)
    {
        CurrentVelocity = velocity;

        _targetVelocity = CurrentVelocity;
        _acceleration = 0;
    }

    public void AccelerateTo(Vector2 targetVel, float accel)
    {
        _targetVelocity = targetVel;
        _acceleration = accel;
    }

    void FixedUpdate()
    {
        Vector2 newVel = Vector2.MoveTowards(CurrentVelocity, _targetVelocity, _acceleration * Time.fixedDeltaTime);
        CurrentVelocity = newVel;
        rb.MovePosition(rb.position + CurrentVelocity * Time.fixedDeltaTime);
    }
}

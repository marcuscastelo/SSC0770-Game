using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityMovement : MonoBehaviour
{   
    [Header("References")]
    
    public Transform affectedTransform;
    public Rigidbody2D affectedRigidbody;

    [SerializeField]
    protected EntityStats entityStats;

    [Space(10)]

    [Header("Event listeners")]

    public UnityEvent<Vector2> onMoveWillChanged;

    [Space(10)]

    [Header("Runtime variables")]

    [SerializeField]
    protected Vector2 movementWill = Vector2.zero;

    public Vector2 CurrentVelocity { get { return affectedRigidbody.velocity; } }

    public void UpdateMovementWill(Vector2 movementWill)
    {
        this.movementWill = movementWill.normalized;
        onMoveWillChanged.Invoke(movementWill);

        //Flip transform if negative movement will 
        if (movementWill.x < 0)
        {
            affectedTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movementWill.x > 0)
        {
            affectedTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        float accel = entityStats.acceleration;
        float maxSpeed = entityStats.maxSpeed;
        float friction = entityStats.friction;

        Vector2 currentVelocity = affectedRigidbody.velocity;

        Vector2 accelVector = movementWill * accel;
        Vector2 newVelocity = (currentVelocity + accelVector * Time.fixedDeltaTime);

        if (movementWill.magnitude == 0) {
            newVelocity *= friction;
        }

        if (newVelocity.magnitude > maxSpeed)
        {
            newVelocity = newVelocity.normalized * maxSpeed;
        }
        else if (newVelocity.magnitude < 10e-3)
        {
            newVelocity = Vector2.zero;
        }

        affectedRigidbody.velocity = newVelocity;

    }
}

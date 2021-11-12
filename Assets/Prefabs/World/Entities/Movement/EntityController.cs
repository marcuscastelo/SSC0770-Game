using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is responsible for controlling the movement of the entity.
/// </summary>
/// <remarks>
/// This component is attached to the entity's root object, along with a rigidbody.
/// </remarks>
public class EntityController : MonoBehaviour
{   
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float deceleration;

    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 inputVector;

    public void Move(Vector2 inputVector) => this.inputVector = inputVector;
    public void Stop() => CurrentVelocity = Vector2.zero;

    void FixedUpdate()
    {
        CurrentVelocity += inputVector * acceleration;
        CurrentVelocity = Vector2.ClampMagnitude(CurrentVelocity, maxSpeed);

        //Apply deceleration
        if (inputVector == Vector2.zero)
            CurrentVelocity = Vector2.MoveTowards(CurrentVelocity, Vector2.zero, deceleration);

        //Apply velocity
        transform.Translate(CurrentVelocity * Time.fixedDeltaTime);
    }

    // public void UpdateMovementWill(Vector2 movementWill)
    // {
    //     this.movementWill = movementWill.normalized;
    //     onMoveWillChanged.Invoke(movementWill);

    //     //Flip transform if negative movement will 
    //     if (movementWill.x < 0)
    //     {
    //         affectedTransform.localScale = new Vector3(-1, 1, 1);
    //     }
    //     else if (movementWill.x > 0)
    //     {
    //         affectedTransform.localScale = new Vector3(1, 1, 1);
    //     }
    // }

    // void FixedUpdate()
    // {
    //     float accel = entityStats.acceleration;
    //     float maxSpeed = entityStats.maxSpeed;
    //     float friction = entityStats.friction;

    //     Vector2 currentVelocity = affectedRigidbody.velocity;

    //     Vector2 accelVector = movementWill * accel;
    //     Vector2 newVelocity = (currentVelocity + accelVector * Time.fixedDeltaTime);

    //     if (movementWill.magnitude == 0) {
    //         newVelocity *= friction;
    //     }

    //     if (newVelocity.magnitude > maxSpeed)
    //     {
    //         newVelocity = newVelocity.normalized * maxSpeed;
    //     }
    //     else if (newVelocity.magnitude < 10e-3)
    //     {
    //         newVelocity = Vector2.zero;
    //     }

    //     affectedRigidbody.velocity = newVelocity;

    // }
}

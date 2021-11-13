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
    [SerializeField, Range(0.001f,  10f)] protected float acceleration;
    [SerializeField                     ] protected float maxSpeed;
    [SerializeField, Range(0,       10f)] protected float deceleration;

    public Vector2 CurrentVelocity { get; protected set; }
    public Vector2 InputVector { get; private set; }

    public float Acceleration { get { return acceleration; } }
    public float MaxSpeed { get { return maxSpeed; } }
    public float Deceleration { get { return deceleration; } }

    /// <summary>
    /// Move according to the input vector, using linear acceleration.
    /// </summary>
    public void Move(Vector2 inputVector)
    {
        this.InputVector = inputVector;
        if (inputVector.sqrMagnitude > 0.1f)
            transform.localScale = new Vector3(Mathf.Sign(inputVector.x), 1, 1); // Flip sprite to face direction of movement
    }

    public void Stop() => CurrentVelocity = Vector2.zero;

    protected void FixedUpdate()
    {
        // Decelerate if no input or if dot product is smaller than 0.707 (45 degrees)
        if (Vector2.Dot(InputVector, CurrentVelocity) < 0.707f)
            CurrentVelocity -= CurrentVelocity.normalized * deceleration * Time.fixedDeltaTime;

        CurrentVelocity += InputVector * acceleration * Time.fixedDeltaTime;
        CurrentVelocity = Vector2.ClampMagnitude(CurrentVelocity, maxSpeed);

        //TODO: Add friction

        //Apply velocity
        transform.Translate(CurrentVelocity * Time.fixedDeltaTime );
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)InputVector);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)CurrentVelocity);
        // Handles.DrawBezier(p1,p2,p1,p2, Color.red,null,thickness);
    }
}

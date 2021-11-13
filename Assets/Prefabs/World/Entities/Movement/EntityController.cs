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
    [SerializeField, Range(0.001f,  100f)] protected float acceleration;
    [SerializeField                     ] protected float maxSpeed;
    [SerializeField, Range(0,       100f)] protected float deceleration;

    public Vector2 CurrentVelocity { get; protected set; }
    public Vector2 InputVector { get; private set; }

    public float Acceleration { get { return acceleration; } }
    public float MaxSpeed { get { return maxSpeed; } }
    public float Deceleration { get { return deceleration; } }

    public Rigidbody2D rb;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

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
        float fixedCorrection = (Time.fixedDeltaTime);
        Debug.Log("Fixed correction: " + fixedCorrection);


        CurrentVelocity += InputVector * (Acceleration + Deceleration) * fixedCorrection;
        CurrentVelocity = Vector2.ClampMagnitude(CurrentVelocity, MaxSpeed);

        if (CurrentVelocity.sqrMagnitude > 0.1f)
            CurrentVelocity -= CurrentVelocity.normalized * Deceleration * fixedCorrection;
        else if (InputVector == Vector2.zero)
            CurrentVelocity = Vector2.zero;
            
        if (rb != null)
            rb.MovePosition(rb.position + CurrentVelocity * fixedCorrection);
    }

    
}

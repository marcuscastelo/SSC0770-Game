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
    [SerializeField, Range(0.001f, 100f)] protected float walkingAcceleration;
    [SerializeField] protected float maxWalkingSpeed;
    [SerializeField, Range(0, 100f)] protected float walkingDeceleration;

    // [SerializeField, Range(0.001f, 100f)] protected float dashAcceleration;
    [SerializeField] protected float maxDashSpeed;
    [SerializeField, Range(0, 100f)] protected float dashDeceleration;

    public Vector2 CurrentVelocity { get; protected set; }
    public Vector2 InputVector { get; private set; }
    public Vector2 LastLookDirection { get; private set; }

    //TODO: this is bad, really bad. easy to forget to set this up.
    private bool hack_isDashing = false;

    // public float Acceleration { get { return hack_isDashing ? dashAcceleration : walkingAcceleration; } }
    public float Acceleration { get { return hack_isDashing ? 0 : walkingAcceleration; } }
    public float MaxSpeed { get { return hack_isDashing ? maxDashSpeed : maxWalkingSpeed; } }
    public float Deceleration { get { return hack_isDashing ? dashDeceleration : walkingDeceleration; } }
    public bool IsDashing { get { return hack_isDashing; } }

    public Rigidbody2D rb;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 inputVector)
    {
        hack_isDashing = false;
        // Debug.Assert(Mathf.Abs(inputVector.sqrMagnitude - 1f) < 0.1f || inputVector == Vector2.zero, "Input vector magnitude must be 1 or 0-vector (i.e normalized), but it is sqrt of" + inputVector.sqrMagnitude);

        this.InputVector = inputVector;
        if (inputVector.sqrMagnitude > 0.1f)
            transform.localScale = new Vector3(Mathf.Sign(inputVector.x), 1, 1); // Flip sprite to face direction of movement

        // Update look direction
        LastLookDirection = inputVector;
    }
    public void StopImmediately() => CurrentVelocity = Vector2.zero;

    public virtual void Dash()
    {
        hack_isDashing = true;
        InputVector = Vector2.zero;
        CurrentVelocity = LastLookDirection * MaxSpeed;
    }

    public virtual void Attack()
    {

    }

    public virtual void Interact()
    {

    }

    protected void FixedUpdate()
    {
        Debug.Log("----------------");
        Debug.Log("CurrentVelocity: " + CurrentVelocity);
        Debug.Log("InputVector: " + InputVector);
        Debug.Log("Dash: " + hack_isDashing);
        Debug.Log("----------------");
        UpdateMovement(Time.fixedDeltaTime);
        UpdatePosition(Time.fixedDeltaTime);
    }

    private void ApplyAcceleration(float deltaTime, bool compensateDeceleration = true)
    {
        CurrentVelocity += InputVector * (Acceleration + Deceleration * (compensateDeceleration ? 1 : 0)) * deltaTime;
        CurrentVelocity = Vector2.ClampMagnitude(CurrentVelocity, MaxSpeed);
    }

    private void ApplyDeceleration(float deltaTime)
    {
        if (CurrentVelocity.sqrMagnitude > 0.1f)
            CurrentVelocity -= Vector2.ClampMagnitude(CurrentVelocity.normalized * Deceleration * deltaTime, CurrentVelocity.magnitude);
    }

    private void UpdateMovement(float deltaTime)
    {
        ApplyAcceleration(deltaTime);
        ApplyDeceleration(deltaTime);
        if (CurrentVelocity.sqrMagnitude < 0.1f) {
            StopImmediately();
            hack_isDashing = false;
        }
    }

    private void UpdatePosition(float deltaTime)
    {
        if (rb != null)
            rb.MovePosition(rb.position + CurrentVelocity * deltaTime);
    }



}

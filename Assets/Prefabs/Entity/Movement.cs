using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float acceleration = 1;
    public float maxSpeed = 20;
    public float friction = 0.9f;

    public Vector2 Velocity { get; set; }

    private Vector2 _moveWill;
    public Vector2 MoveWill { 
        get { return _moveWill; }
        set { _moveWill = value.normalized; }
    }

    // Start is called before the first frame update
    void Start()
    {
        MoveWill = Vector2.zero;
        Velocity = Vector2.zero;
    }

    void FixedUpdate() {    
        // Apply acceleration
        Velocity += MoveWill * acceleration * Time.fixedDeltaTime;

        // Apply friction
        Velocity *= friction;

        // Limit speed
        if (Velocity.magnitude > maxSpeed) {
            Velocity = Velocity.normalized * maxSpeed;
        }

        // Apply velocity
        rb.MovePosition(rb.position + Velocity * Time.fixedDeltaTime);
    }
}

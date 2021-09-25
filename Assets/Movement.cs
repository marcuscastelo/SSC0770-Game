using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float acceleration = 1;
    public float maxSpeed = 20;
    public float friction = 0.9f;

    public Vector2 Velocity { get; private set; }
    private Vector2 controllerInput;


    // Start is called before the first frame update
    void Start()
    {
        controllerInput = Vector2.zero;
        Velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {   
        controllerInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) {
            controllerInput.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            controllerInput.y -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            controllerInput.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            controllerInput.x += 1;
        }
    }

    void FixedUpdate() {    

        // Apply acceleration
        Velocity += controllerInput * acceleration * Time.fixedDeltaTime;

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

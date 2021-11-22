using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBaseEntity : MonoBehaviour
{
    public EntityController controller;

    private Vector2 inputVector = Vector2.right;
    
    private static float BASE_FPS = 60;

    [InspectorName("Lag Test (Drag to change radius)"), Range(0.01f, 2f)]
    public float radius = 5;

    void FixedUpdate()
    {
        // float mult = BASE_FPS * Time.fixedDeltaTime;

        // float maxLinearSpeed = controller.MaxSpeed;
        // float angularSpeed = maxLinearSpeed / radius;

        // inputVector = Quaternion.Euler(0, 0, angularSpeed * mult) * inputVector;

        // float centripetalAcceleration = angularSpeed * angularSpeed / radius;

        // float ratio = centripetalAcceleration / controller.Acceleration;

        // controller.Walk(inputVector * ratio);
    }

    void OnDrawGizmos()
    {
        Vector2 perpendicular = Vector2.Perpendicular(inputVector);
        Vector2 circleCenter = (Vector2)transform.position + perpendicular * radius;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCenter, radius);
    }
}

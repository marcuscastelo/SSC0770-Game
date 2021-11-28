using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Entities.Systems;

public class RotatingBaseEntity : MonoBehaviour
{
    public EntityMovement movement;

    private Vector2 tangentVector = Vector2.right;
    
    [InspectorName("Lag Test (Drag to change radius)"), Range(0.01f, 2f)]
    public float radius = 5;
    public float period = 1f;

    void FixedUpdate()
    {
        float FPS = 1f / Time.fixedDeltaTime;
        float angularSpeedPerFrame = (360f / FPS) / period;

        tangentVector = Quaternion.Euler(0, 0, angularSpeedPerFrame) * tangentVector;
        float linearSpeedPerFrame = radius * angularSpeedPerFrame;
        float linearSpeed = linearSpeedPerFrame / FPS;
        movement.SetVel(tangentVector * linearSpeed);
    }

    void OnDrawGizmos()
    {
        Vector2 perpendicular = Vector2.Perpendicular(tangentVector);
        Vector2 circleCenter = (Vector2)transform.position + perpendicular * radius;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCenter, radius);
    }
}

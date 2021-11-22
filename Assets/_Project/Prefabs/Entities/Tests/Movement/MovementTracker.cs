using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTracker : MonoBehaviour
{
    [Header("Points")]
    public bool showPoints = true;
    public int everyXFrames = 1;
    public int maxPoints = 100;

    [Header("Movement")]
    public bool showMovement = true;
    public EntityController entityController;
    

    private int frameCounter = 0;
    private readonly List<Vector3> positions = new List<Vector3>();

    void FixedUpdate()
    {
        frameCounter++;
        if (frameCounter >= everyXFrames)
        {
            frameCounter = 0;
            positions.Add(transform.position);
            while (positions.Count > maxPoints)
                positions.RemoveAt(0);
        }
    }

    void OnDrawGizmos()
    {
        if (showPoints) {
            Gizmos.color = Color.red;
            foreach (var position in positions)
            {
                Gizmos.DrawSphere(position, 0.1f);
            }
        }
        
        if (showMovement) {
            Gizmos.color = Color.green;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)entityController.InputVector);
            Gizmos.color = Color.red;
            Vector3 offsetedBase = transform.position + new Vector3(0, 0.05f, 0);
            Gizmos.DrawLine(offsetedBase, offsetedBase + (Vector3)entityController.CurrentVelocity);
        }
        // Handles.DrawBezier(p1,p2,p1,p2, Color.red,null,thickness);
    }
}

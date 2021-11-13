using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTracker : MonoBehaviour
{
    public int everyXFrames = 1;
    public int maxPoints = 100;


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
        Gizmos.color = Color.red;
        foreach (var position in positions)
        {
            Gizmos.DrawSphere(position, 0.1f);
        }

        Gizmos.color = Color.green;
    }
}

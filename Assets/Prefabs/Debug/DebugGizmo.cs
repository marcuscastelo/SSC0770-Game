using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DebugGizmo : MonoBehaviour
{
    public bool show = true;
    public float size = 0.1f;
    public Color color = Color.white;

    void OnDrawGizmos()
    {
        if (!show)
            return;

        var parent = transform.parent;

        string parentObjectName;
        if (parent == null)
            parentObjectName = "DEBUG: NO PARENT ATTACHED!";
        else
            parentObjectName = parent.name;
            
        //Display the name of the object the script is attached to on the Editor

        GUIStyle labelStyle = new GUIStyle() { 
            normal = new GUIStyleState() { textColor = color }, 
            hover = new GUIStyleState() { textColor = Color.white },
            fontStyle = FontStyle.Bold, 
            fontSize = 12,
            alignment = TextAnchor.MiddleCenter,

        };
        Handles.Label(transform.position, parentObjectName, labelStyle);

        //Draw a cube at entity's position
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position, new Vector3(size, size, size));
    }
}

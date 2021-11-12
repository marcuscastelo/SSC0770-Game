using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBaseEntity : MonoBehaviour
{
    public EntityController controller;

    private Vector2 inputVector = Vector2.right;

    // Update is called once per frame
    void Update()
    {
        inputVector = Quaternion.Euler(0, 0, 1) * inputVector;
        controller.Move(inputVector);
        Debug.DrawLine(transform.position, transform.position + (Vector3)inputVector, Color.red);
    }
}

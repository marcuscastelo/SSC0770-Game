using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    void Update()
    {
        Vector2 controllerInput = Vector2.zero;
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
        movement.MoveWill = controllerInput;
    }
}

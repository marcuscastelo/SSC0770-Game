using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    void Update()
    {
        //TODO: Use key aliases instead of hardcoded values

        //Checks if player is pressing Z (attack key)
        if (Input.GetKeyDown(KeyCode.Z))
            state.wantsToAttack = true; 

        //Gets player movement will.
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

        state.movementWill = controllerInput.normalized;
    }
}

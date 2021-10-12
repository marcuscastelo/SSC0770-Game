using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    public Collider2D interactionCollider;

    void Update()
    {
        //TODO: Use key aliases instead of hardcoded values

        //Checks if player is pressing Z (attack key)
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.F))
            state.wantsToAttack = true; 

        //Checks if player is pressing E (interact key)
        if (Input.GetKeyDown(KeyCode.E))
            state.wantsToInteract = true;
        if (Input.GetKeyUp(KeyCode.E))
            state.wantsToInteract = false;

        //Gets player movement will.
        Vector2 controllerInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            controllerInput.y += 1;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            controllerInput.y -= 1;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            controllerInput.x -= 1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            controllerInput.x += 1;
        }

        state.movementWill = controllerInput.normalized;
    }
}

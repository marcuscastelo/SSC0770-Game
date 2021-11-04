using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public EntityMovement entityMovement;
    public PlayerInteraction playerInteraction;

    [Header("Settings")]
    public UnityEngine.KeyCode interactKey = KeyCode.E;

    void Awake()
    {
        if (entityMovement == null)
            entityMovement = GetComponent<EntityMovement>();
        if (playerInteraction == null)
            playerInteraction = GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMovementWill();
        InputInteract();
    }

    void InputMovementWill()
    {
        Vector2 newMovementWill = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        entityMovement.UpdateMovementWill(newMovementWill);
    }

    void InputInteract()
    {
        if (Input.GetKeyDown(interactKey))
            playerInteraction.InteractWithSelectedObject();
    }
}

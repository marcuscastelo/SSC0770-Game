using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityController : MonoBehaviour
{
    [SerializeField] private AreaInteractor interactor;
    [SerializeField] private EntityCombat combat;
    [SerializeField] private EntityMovement movement;

    private void Awake()
    {
        Debug.Assert(interactor != null, "EntityController.Awake() - _interactor is null");
        Debug.Assert(combat != null, "EntityController.Awake() - _attacker is null");
        Debug.Assert(movement != null, "EntityController.Awake() - movement is null");
    }

    public void Interact() => interactor.Interact();
    public void Attack() => combat.Attack();

    public void Walk(Vector2 direction)
    {
        movement.Move(direction);

        if (direction.sqrMagnitude > 0.1f)
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1); // Flip sprite to face direction of movement
    }

    public void Dash() => movement.Move(movement.LastLookDirection);       
}

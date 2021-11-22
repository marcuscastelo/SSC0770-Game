using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityController : MonoBehaviour
{
    [SerializeField] private AreaInteractor interactor;
    [SerializeField] private EntityCombat combat;
    [SerializeField] private EntityMovement movement;
    [SerializeField] private Transform entityTransform;
    [SerializeField] private Animator animator;

    public Vector2 InputDirection { get; private set; }

    enum State { Moving, Attacking, Interacting, Dashing, Dead }
    State _state = State.Moving;

    private void Awake()
    {
        Debug.Assert(interactor != null, "EntityController.Awake() - _interactor is null");
        Debug.Assert(combat != null, "EntityController.Awake() - _attacker is null");
        Debug.Assert(movement != null, "EntityController.Awake() - movement is null");
    }

    public void Interact() => StartCoroutine(InteractCoroutine());
    public void Attack() => StartCoroutine(AttackCoroutine());
    public void Move(Vector2 direction) => StartCoroutine(MoveCoroutine(direction));
    public void Dash(Vector2 direction) => StartCoroutine(DashCoroutine(direction));
    public void Dash() => StartCoroutine(DashCoroutine(InputDirection));

    public IEnumerator InteractCoroutine()
    {
        if (_state != State.Moving)
            yield break;

        interactor.Interact();
        yield return new WaitForSeconds(5f);

        _state = State.Moving;

        yield break;
    }

    public IEnumerator AttackCoroutine()
    {
        if (_state != State.Moving)
            yield break;

        _state = State.Attacking;
        combat.Attack();
        animator.SetTrigger("attackTrigger");

        yield return new WaitForSeconds(combat.Stats.attackDuration);
        
        _state = State.Moving;
        yield break;
    }

    public IEnumerator MoveCoroutine(Vector2 direction)
    {
        if (_state != State.Moving)
            yield break;

        InputDirection = direction;
        movement.AccelerateTo(direction, movement.Stats.acceleration);
        UpdateAnimator();

        yield break;
    }

    public IEnumerator DashCoroutine(Vector2 direction)
    {
        if (_state != State.Moving)
            yield break;

        InputDirection = direction;
        movement.SetVel(direction * movement.Stats.dashSpeed);
        UpdateAnimator();

        yield return new WaitForSeconds(movement.Stats.dashDuration);

        movement.SetVel(Vector2.zero);
        UpdateAnimator();

        _state = State.Moving;
        yield break;
    }

    private void UpdateAnimator()
    {
        entityTransform.localScale = new Vector3(Mathf.Sign(InputDirection.x), 1, 1); // Flip sprite to face direction of movement

        animator.SetFloat("speedX", InputDirection.x);
        animator.SetFloat("speedY", InputDirection.y);
    }
}

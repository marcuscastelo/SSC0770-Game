using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Hypnos.Entities.Systems;

namespace Hypnos.Entities
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] private AreaInteractor interactor;
        [SerializeField] private EntityCombat combat;
        [SerializeField] private EntityMovement movement;
        [SerializeField] private Transform entityTransform;
        [SerializeField] private Animator animator;

        public Vector2 InputDirection { get; private set; }
        public Vector2 LastLookDirection { get; private set; }
        public Vector2 CurrentVelocity => movement.CurrentVelocity;

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
        public void Dash() => StartCoroutine(DashCoroutine(LastLookDirection));
        public void LookTo(Vector2 direction)
        {
            LastLookDirection = direction;
            UpdateAnimator(LastLookDirection.normalized * 0.1f);
        }

        public IEnumerator InteractCoroutine()
        {
            if (_state != State.Moving)
                yield break;

            interactor.Interact();
            yield return new WaitForSeconds(5f);

            _state = State.Moving;

            yield break;
        }

        private float _attackLastTime = float.MinValue;
        public IEnumerator AttackCoroutine()
        {
            if (_state != State.Moving)
                yield break;

            if (Time.time - _attackLastTime < combat.Stats.attackCooldown)
                yield break;

            _state = State.Attacking;
            movement.SetVel(Vector2.zero);
            UpdateAnimator(Vector2.zero);

            animator.SetFloat("attackSpeed", (1f / combat.Stats.attackDuration) * combat.Stats.attackAnimatorMultiplier);
            animator.SetTrigger("attackTrigger");
            combat.Attack();

            yield return new WaitForSeconds(combat.Stats.attackDuration);
            UpdateAnimator(InputDirection);

            _state = State.Moving;
            _attackLastTime = Time.time;
            yield break;
        }

        public IEnumerator MoveCoroutine(Vector2 direction)
        {
            InputDirection = direction;
            if (InputDirection.sqrMagnitude > 0)
                LastLookDirection = InputDirection;

            if (_state != State.Moving)
                yield break;

            UpdateAnimator(InputDirection);

            yield break;
        }

        private float _dashLastTime = float.MinValue;
        public IEnumerator DashCoroutine(Vector2 direction)
        {
            if (_state != State.Moving)
                yield break;

            if (Time.time - _dashLastTime < movement.DashStats.dashCooldown)
                yield break;

            _state = State.Dashing;
            animator.SetTrigger("dashTrigger");
            movement.OnDashStart();
            UpdateAnimator(Vector2.zero);

            movement.SetVel(direction * movement.DashStats.dashSpeed);

            yield return new WaitForSeconds(movement.DashStats.dashDuration);
            movement.OnDashEnd();
            movement.SetVel(Vector2.zero);
            UpdateAnimator(InputDirection);

            _state = State.Moving;
            _dashLastTime = Time.time;
            yield break;
        }

        private void UpdateAnimator(Vector2 lookDirection)
        {
            if (lookDirection != Vector2.zero)
                entityTransform.localScale = new Vector3(Mathf.Sign(lookDirection.x), 1, 1); // Flip sprite to face direction of movement

            animator.SetFloat("speedX", lookDirection.x);
            animator.SetFloat("speedY", lookDirection.y);
        }

        private void FixedUpdate()
        {
            if (_state == State.Moving)
                movement.AccelerateTo(InputDirection * movement.WalkStats.maxSpeed, movement.WalkStats.acceleration);
        }
    }
}
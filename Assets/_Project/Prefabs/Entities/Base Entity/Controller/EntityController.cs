using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Hypnos.Core;

namespace Hypnos.Entities
{
    public class EntityController : MonoBehaviour
    {
        private IInteractor _interactor;
        private IAttacker _attacker;
        private IMoveable _movement;
        private Entity _entity;

        public Vector2 InputDirection { get; private set; }
        public Vector2 LastLookDirection { get; private set; }
        public Vector2 CurrentVelocity => _movement.CurrentVelocity;

        enum State { Moving, Attacking, Interacting, Dashing, Dead }
        State _state = State.Moving;

        [Inject]
        public void Construct(IInteractor interactor, IAttacker attacker, IMoveable movement, Entity entity)
        {
            _interactor = interactor;
            _attacker = attacker;
            _movement = movement;
            _entity = entity;
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
        
        public bool CanMove() => _state == State.Moving;
        public bool CanInteract() => _state == State.Moving;
        public bool CanAttack() => _state == State.Moving;
        public bool CanDash() => _state == State.Moving;

        public IEnumerator InteractCoroutine()
        {
            if (!CanInteract())
                yield break;

            _state = State.Interacting;
            _interactor.Interact((bool _) => _state = State.Moving);

            yield break;
        }

        private float _attackLastTime = float.MinValue;
        public IEnumerator AttackCoroutine()
        {
            if (!CanAttack())
                yield break;

            // if (Time.time - _attackLastTime < _attacker.Stats.attackCooldown)
            //     yield break;

            // _state = State.Attacking;
            // _movement.SetVel(Vector2.zero);
            // UpdateAnimator(Vector2.zero);

            // _animator.SetFloat("attackSpeed", (1f / _attacker.Stats.attackDuration) * _attacker.Stats.attackAnimatorMultiplier);
            // _animator.SetTrigger("attackTrigger");
            // _attacker.Attack();

            // yield return new WaitForSeconds(_attacker.Stats.attackDuration);
            // UpdateAnimator(InputDirection);

            // _state = State.Moving;
            // _attackLastTime = Time.time;
            // yield break;
        }

        public IEnumerator MoveCoroutine(Vector2 direction)
        {
            // Update direction, even if we can't move
            InputDirection = direction;
            if (InputDirection.sqrMagnitude > 0)
                LastLookDirection = InputDirection;

            if (!CanMove())
                yield break;

            UpdateAnimator(InputDirection);
            // FixedUpdate is called every frame with the InputDirection set

            yield break;
        }

        private float _dashLastTime = float.MinValue;
        public IEnumerator DashCoroutine(Vector2 direction)
        {
            if (!CanDash())
                yield break;

            // if (Time.time - _dashLastTime < _movement.DashStats.dashCooldown)
            //     yield break;

            // _state = State.Dashing;
            // _animator.SetTrigger("dashTrigger");
            // _movement.OnDashStart();
            // UpdateAnimator(Vector2.zero);

            // _movement.SetVel(direction * _movement.DashStats.dashSpeed);

            // yield return new WaitForSeconds(_movement.DashStats.dashDuration);
            // _movement.OnDashEnd();
            // _movement.SetVel(Vector2.zero);
            // UpdateAnimator(InputDirection);

            // _state = State.Moving;
            // _dashLastTime = Time.time;
            // yield break;
        }

        private void UpdateAnimator(Vector2 lookDirection)
        {
            if (lookDirection != Vector2.zero)
                _entity.transform.localScale = new Vector3(Mathf.Sign(lookDirection.x), 1, 1); // Flip sprite to face direction of movement

            _entity.Animator.SetFloat("speedX", lookDirection.x);
            _entity.Animator.SetFloat("speedY", lookDirection.y);
        }

        private void FixedUpdate()
        {
            // if (_state == State.Moving)
            //     _movement.AccelerateTo(InputDirection * _movement.WalkStats.maxSpeed, _movement.WalkStats.acceleration);
        }
    }
}
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
        private IAttackable _attackable;
        private IMoveable _movement;
        private Entity _entity;

        public Vector2 InputDirection { get; private set; }
        public Vector2 LastLookDirection { get; private set; }
        public Vector2 CurrentVelocity => _movement.CurrentVelocity;

        enum State { Moving, Attacking, Interacting, Dashing, Dead }
        [SerializeField] [ReadOnly] private State _state = State.Moving;

        [Inject]
        public void Construct(Entity entity, IInteractor interactor, IAttacker attacker, IAttackable attackable, IMoveable movement)
        {
            _entity = entity;
            _interactor = interactor;
            _attacker = attacker;
            _attackable = attackable;
            _movement = movement;
        }

        public void Interact()
        {
            if (isActiveAndEnabled)
                StartCoroutine(InteractCoroutine());
        }
        public void Attack()
        {
            if (isActiveAndEnabled)
                StartCoroutine(AttackCoroutine());
        }
        public void Move(Vector2 direction)
        {
            if (isActiveAndEnabled)
                StartCoroutine(MoveCoroutine(direction));
        }
        public void Dash(Vector2 direction)
        {
            if (isActiveAndEnabled)
                StartCoroutine(DashCoroutine(direction));
        }
        public void Dash()
        {
            if (isActiveAndEnabled)
                StartCoroutine(DashCoroutine(LastLookDirection));
        }
        public void LookTo(Vector2 direction)
        {
            if (!isActiveAndEnabled) return;
            
            LastLookDirection = direction;
            UpdateAnimator(LastLookDirection.normalized * 0.1f);
        }

        public bool CanMove() => _state == State.Moving;
        public bool CanInteract() => _state == State.Moving;
        public bool CanAttack() => _state == State.Moving;
        public bool CanDash() => _entity.HasBuff(Buff.Dash) && _state == State.Moving;

        public IEnumerator InteractCoroutine()
        {
            if (!CanInteract())
                yield break;

            _state = State.Interacting;
            _movement.SetVel(Vector2.zero);
            UpdateAnimator(Vector2.zero);
            _interactor.Interact(_ =>
            {
                _state = State.Moving;
                UpdateAnimator(InputDirection);
            });

            yield break;
        }

        private float _attackLastTime = float.MinValue;
        public IEnumerator AttackCoroutine()
        {
            if (!CanAttack())
                yield break;

            if (Time.time - _attackLastTime < _entity.CombatStats.attackCooldown)
                yield break;

            _state = State.Attacking;
            _movement.SetVel(Vector2.zero);
            UpdateAnimator(Vector2.zero);

            float speedAttackBuffMultiplier = _entity.HasBuff(Buff.Dexterity) ? 1.5f : 1f; //TODO: modularize
            _entity.Animator.SetFloat("attackSpeed", (1f / _entity.CombatStats.attackDuration) * _entity.CombatStats.attackAnimatorMultiplier * speedAttackBuffMultiplier);
            _entity.Animator.SetTrigger("attackTrigger");
            _attacker.Attack();

            yield return new WaitForSeconds(_entity.CombatStats.attackDuration / speedAttackBuffMultiplier);
            UpdateAnimator(InputDirection);

            _state = State.Moving;
            _attackLastTime = Time.time;
            yield break;
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

            if (Time.time - _dashLastTime < _entity.DashStats.dashCooldown)
                yield break;

            _state = State.Dashing;
            _attackable.SetInvulnerable(true);
            _entity.Animator.SetTrigger("dashTrigger");
            UpdateAnimator(Vector2.zero);

            _movement.SetVel(direction * _entity.DashStats.dashSpeed);

            yield return new WaitForSeconds(_entity.DashStats.dashDuration);
            _movement.SetVel(Vector2.zero);
            _attackable.SetInvulnerable(false);
            UpdateAnimator(InputDirection);

            _state = State.Moving;
            _dashLastTime = Time.time;
            yield break;
        }

        private void UpdateAnimator(Vector2 animDir)
        {
            if (animDir != Vector2.zero)
                _entity.transform.localScale = new Vector3(Mathf.Sign(animDir.x), 1, 1); // Flip sprite to face direction of movement

            _entity.Animator.SetFloat("speedX", animDir.x);
            _entity.Animator.SetFloat("speedY", animDir.y);
        }

        private void FixedUpdate()
        {
            if (_state == State.Moving)
                _movement.AccelerateTo(InputDirection * _entity.WalkStats.maxSpeed, _entity.WalkStats.acceleration);
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Hypnos.Core;
using Hypnos.Audio;
using Zenject;

namespace Hypnos.Entities.Systems
{
    public class EntityCombat : MonoBehaviour, IAttackable, IAttacker
    {
        [SerializeField] private Collider2D attackerArea;
        [SerializeField] private bool attackAreaAlwaysEnabled = false;
        [SerializeField] private bool invulnerable = false;

        [SerializeField] private int startFrame = 0;
        [SerializeField] private int endFrame = 1;
        [SerializeField] private int totalFrames = 1;

        private Entity _thisEntity;
        private AudioSystem _audioSystem;
        private AudioSource _audioSource;
        private bool _attacking;
        private float _lastAttackTime = float.MinValue;

        [Inject]
        public void Construct(Entity thisEntity, AudioSystem audioSystem)
        {
            _thisEntity = thisEntity;
            _audioSystem = audioSystem;
        }

        public void SetInvulnerable(bool invulnerable)
        {
            this.invulnerable = invulnerable;
        }

        void Start()
        {
            if (attackerArea != null)
            {
                attackerArea.enabled = attackAreaAlwaysEnabled;
                attackerArea.transform.localScale = Vector3.one * (attackAreaAlwaysEnabled ? 1 : 0);
            }

            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
        }

        private IEnumerator BlinkAttackAreaCoroutine(float duration)
        {
            if (!CanAttack())
                yield break;
            _attacking = true;
            float speedAttackBuffMultiplier = _thisEntity.HasBuff(Buff.Dexterity) ? 1.5f : 1f; //TODO: modularize
            duration /= speedAttackBuffMultiplier;
            float frameDuration = duration / totalFrames;
            if (attackerArea != null && !attackAreaAlwaysEnabled)
            {
                attackerArea.transform.localScale = Vector3.zero;

                if (_thisEntity.HasBuff(Buff.Damage))
                    _audioSystem.PlayAudio(AudioType.SFX_Hypnos_Attack_Sword_Miss, _audioSource);
                else
                    _audioSystem.PlayAudio(AudioType.SFX_Hypnos_Attack_Hand_Miss, _audioSource);

                
                yield return new WaitForSeconds(startFrame * frameDuration);

                attackerArea.enabled = true;
                attackerArea.transform.localScale = Vector3.one;

                yield return new WaitForSeconds((endFrame - startFrame) * frameDuration);
                attackerArea.enabled = false;
            }
            _attacking = false;
            _lastAttackTime = Time.time;
        }

        public void Attack() => StartCoroutine(BlinkAttackAreaCoroutine(_thisEntity.CombatStats.attackDuration));

        public void OnHurt(IAttacker attacker, int damage)
        {
            if (invulnerable) return;

            //TODO: knockback
            _thisEntity.Health.TakeDamage(damage);
            StartCoroutine(BlinkAttackAreaCoroutine(3f));
        }

        private IEnumerator BlinkHurtRedCoroutine(float duration)
        {
            Debug.Log("BlinkHurtRedCoroutine called");
            SpriteRenderer spriteRenderer = _thisEntity.SpriteRenderer;
            Color originalColor = spriteRenderer.color;
            Color newColor = new Color(1, 0, 0, 1);
            spriteRenderer.color = newColor;
            yield return new WaitForSeconds(duration);
            Debug.Log("BlinkHurtRedCoroutine time: " + duration);
            spriteRenderer.color = originalColor;

            yield return null;
        }

        public void OnValidate()
        {
            if (attackerArea != null)
                attackerArea.enabled = attackAreaAlwaysEnabled;

            if (startFrame >= endFrame)
            {
                Debug.LogWarning("EntityCombat.OnValidate() - startFrame must be less than endFrame");
                endFrame = startFrame + 1;
            }

            if (endFrame >= totalFrames)
            {
                Debug.LogWarning("EntityCombat.OnValidate() - endFrame must be less than or equal to totalFrames");
            }

            totalFrames = Mathf.Max(totalFrames, endFrame + 1);
        }

        public void OnHurtboxEnter(Collider2D other)
        {
        }

        public void OnHurtboxExit(Collider2D other)
        {
        }

        public void OnHitboxEnter(Collider2D other)
        {
            IAttackable attackable = other.GetComponentInParent<IAttackable>();
            if (attackable == null) {
                Debug.LogWarning("EntityCombat.OnHitboxEnter() - other.GetComponentInParent<IAttackable>() returned null");
                return;
            }

            if (_thisEntity.HasBuff(Buff.Damage))
                _audioSystem.PlayAudio(AudioType.SFX_Hypnos_Attack_Sword_Hit, _audioSource);
            else
                _audioSystem.PlayAudio(AudioType.SFX_Hypnos_Attack_Hand_Hit, _audioSource);
            attackable.OnHurt(this, _thisEntity.Stats.combatStats.attackDamage); //TODO: use facade
        }

        public void OnHitboxExit(Collider2D other)
        {
        }

        public bool CanAttack()
        {
            return !_attacking && Time.time - _lastAttackTime > _thisEntity.CombatStats.attackCooldown;
        }

    }
}
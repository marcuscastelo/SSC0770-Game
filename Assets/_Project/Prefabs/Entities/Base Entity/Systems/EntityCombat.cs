using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Hypnos.Core;
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

        [Inject]
        public void Construct(Entity thisEntity)
        {
            _thisEntity = thisEntity;
        }

        public void SetInvulnerable(bool invulnerable)
        {
            this.invulnerable = invulnerable;
        }

        void Start()
        {
            if (attackerArea != null) {
                attackerArea.enabled = attackAreaAlwaysEnabled;
                attackerArea.transform.localScale = Vector3.one * (attackAreaAlwaysEnabled? 1 : 0);
            }
        }

        private IEnumerator BlinkAttackAreaCoroutine(float duration)
        {
            float speedAttackBuffMultiplier = _thisEntity.HasBuff(Buff.Dexterity) ? 1.5f : 1f; //TODO: modularize
            duration /= speedAttackBuffMultiplier;
            float frameDuration = duration / totalFrames;
            if (attackerArea != null && !attackAreaAlwaysEnabled)
            {
                attackerArea.transform.localScale = Vector3.zero;
                yield return new WaitForSeconds(startFrame * frameDuration);
                attackerArea.enabled = true;
                attackerArea.transform.localScale = Vector3.one;
                yield return new WaitForSeconds((endFrame - startFrame) * frameDuration);
                attackerArea.enabled = false;
            }
        }

        public void Attack() => StartCoroutine(BlinkAttackAreaCoroutine(_thisEntity.CombatStats.attackDuration));

        public void OnHurt(IAttacker attacker, int damage)
        {
            if (invulnerable) return;

            //TODO: knockback
            _thisEntity.Health.TakeDamage(damage);
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
            Entity entity = other.GetComponentInParent<Entity>();
            if (entity != null)
                entity.AttackableSystem.OnHurt(this, _thisEntity.Stats.combatStats.attackDamage); //TODO: use facade
        }

        public void OnHitboxExit(Collider2D other)
        {
        }

    }
}
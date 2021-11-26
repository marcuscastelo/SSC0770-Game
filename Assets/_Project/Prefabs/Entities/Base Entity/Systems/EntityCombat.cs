using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Hypnos.Entities.Systems
{
    public class EntityCombat : MonoBehaviour, IAttackable, IAttacker
    {
        [SerializeField] private CombatStats combatStats;
        [SerializeField] private Collider2D attackerArea;
        [SerializeField] private bool attackAreaAlwaysEnabled = false;

        [SerializeField] private int startFrame = 0;
        [SerializeField] private int endFrame = 1;
        [SerializeField] private int totalFrames = 1;

        [SerializeField] private EntityMovement entityMovement;

        public CombatStats Stats => combatStats;

        private Entity _thisEntity;

        void Awake()
        {
            if (attackerArea != null)
                attackerArea.enabled = attackAreaAlwaysEnabled;

            Debug.Assert(startFrame < endFrame, "EntityCombat.Awake() - startFrame must be less than endFrame");
            Debug.Assert(endFrame <= totalFrames, "EntityCombat.Awake() - endFrame must be less than or equal to totalFrames");
        }

        void Start()
        {
            if (attackerArea != null) {
                attackerArea.enabled = attackAreaAlwaysEnabled;
                attackerArea.transform.localScale = Vector3.one * (attackAreaAlwaysEnabled? 1 : 0);
            }

            _thisEntity = GetComponentInParent<Entity>();
            Debug.Assert(_thisEntity != null, "EntityCombat.Start() - _thisEntity is null");
        }

        private IEnumerator BlinkAttackAreaCoroutine(float duration)
        {
            float frameDuration = duration / totalFrames;
            if (attackerArea != null && !attackAreaAlwaysEnabled)
            {
                yield return new WaitForSeconds(startFrame * frameDuration);
                attackerArea.enabled = true;
                attackerArea.transform.localScale = Vector3.one;
                yield return new WaitForSeconds((endFrame - startFrame) * frameDuration);
                attackerArea.enabled = false;
                attackerArea.transform.localScale = Vector3.zero;
            }
        }

        public void Attack()
        {
            StartCoroutine(BlinkAttackAreaCoroutine(combatStats.attackDuration));
        }

        public void OnHurt(IAttacker attacker)
        {
            //TODO: knockback
            const int DUMMY_DAMAGE = 1; //TODO: remove this
            _thisEntity.Health.TakeDamage(DUMMY_DAMAGE);
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
                entity.Combat.OnHurt(this);
        }

        public void OnHitboxExit(Collider2D other)
        {
        }

    }
}
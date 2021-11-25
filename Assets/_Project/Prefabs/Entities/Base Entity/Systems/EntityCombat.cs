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

        void Awake()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("EntityCombat: No Rigidbody2D found on " + gameObject.name);
            }

            if (attackerArea != null)
                attackerArea.enabled = attackAreaAlwaysEnabled;

            Debug.Assert(startFrame < endFrame, "EntityCombat.Awake() - startFrame must be less than endFrame");
            Debug.Assert(endFrame <= totalFrames, "EntityCombat.Awake() - endFrame must be less than or equal to totalFrames");
        }

        void Start()
        {
            if (attackerArea != null)
                attackerArea.enabled = attackAreaAlwaysEnabled;
        }

        private IEnumerator BlinkAttackAreaCoroutine(float duration)
        {
            float frameDuration = duration / totalFrames;
            if (attackerArea != null && !attackAreaAlwaysEnabled)
            {
                yield return new WaitForSeconds(startFrame * frameDuration);
                attackerArea.enabled = true;
                yield return new WaitForSeconds((endFrame - startFrame) * frameDuration);
                attackerArea.enabled = false;
            }
        }

        public void Attack()
        {
            StartCoroutine(BlinkAttackAreaCoroutine(combatStats.attackDuration));
        }

        public void OnHurt(IAttacker attacker)
        {
            if (attacker is MonoBehaviour mb)
            {
                var diffVec = transform.position - mb.transform.position;
                var diff = diffVec.magnitude;
                var dir = diffVec.normalized;
                var speed = 3 / (1 + diff);
                entityMovement.SetVel(speed * dir);
            }
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

        public void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.GetComponentInParent<IAttackable>()?.OnHurt(this);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
        }
    }
}
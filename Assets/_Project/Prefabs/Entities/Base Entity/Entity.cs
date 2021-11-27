using UnityEngine;

using Hypnos.Entities.Systems;
using Hypnos.Entities.Components;

using Zenject;
using Hypnos.Core;

namespace Hypnos.Entities
{
    [ExecuteInEditMode]
    public class Entity : MonoBehaviour, IBuffable//, //TODO: IAttackable, ...
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;

        [Header("Entity Components")]
        private HealthComponent _health;
        private IBuffable _buff;

        [Header("Entity Systems")]
        [SerializeField] private EntityController entityController;
        [SerializeField] private EntityMovement entityMovement;
        [SerializeField] private EntityCombat entityCombat;
        [SerializeField] private AreaInteractor entityInteractor;

        // Facade - IBuffable
        public Buff ActiveBuff => _buff.ActiveBuff;
        public void ApplyBuff(Buff buff) => _buff.ApplyBuff(buff);
        public void RemoveBuff(Buff buff) => _buff.RemoveBuff(buff);
        public bool HasBuff(Buff buff) => _buff.HasBuff(buff);
        public void ClearBuffs() => _buff.ClearBuffs();

        // // // Facade - IAttackable
        // // public void OnHurt(int damage) => entityCombat.OnHurt(damage);
    
        public HealthComponent Health => _health;

        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Animator Animator => animator;

        public EntityController Controller => entityController;
        public EntityMovement Movement => entityMovement;
        public EntityCombat Combat => entityCombat;
        public AreaInteractor Interactor => entityInteractor;

        [Inject]
        public void Construct(HealthComponent health, IBuffable buff)
        {
            _health = health;
            _buff = buff;
        }

        [ContextMenu("Update Refs")]
        public void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponentInChildren<Animator>();

            entityController = GetComponentInChildren<EntityController>();
            entityMovement = GetComponentInChildren<EntityMovement>();
            entityCombat = GetComponentInChildren<EntityCombat>();
            entityInteractor = GetComponentInChildren<AreaInteractor>();
        }

    }

}
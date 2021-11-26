using UnityEngine;

using Hypnos.Entities.Systems;
using Hypnos.Entities.Components;

namespace Hypnos.Entities
{
    [ExecuteInEditMode]
    public class Entity : MonoBehaviour
    {
        [Header("Entity Components")]
        [SerializeField] private HealthComponent entityHealth;
        [SerializeField] private BuffComponent entityBuff;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;

        [Header("Entity Systems")]
        [SerializeField] private EntityController entityController;
        [SerializeField] private EntityMovement entityMovement;
        [SerializeField] private EntityCombat entityCombat;
        [SerializeField] private AreaInteractor entityInteractor;

        public HealthComponent Health => entityHealth;
        public BuffComponent Buff => entityBuff;


        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Animator Animator => animator;

        public EntityController Controller => entityController;
        public EntityMovement Movement => entityMovement;
        public EntityCombat Combat => entityCombat;
        public AreaInteractor Interactor => entityInteractor;

        [ContextMenu("Update Refs")]
        public void Awake()
        {
            entityHealth = GetComponentInChildren<HealthComponent>();
            entityBuff = GetComponentInChildren<BuffComponent>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponentInChildren<Animator>();

            entityController = GetComponentInChildren<EntityController>();
            entityMovement = GetComponentInChildren<EntityMovement>();
            entityCombat = GetComponentInChildren<EntityCombat>();
            entityInteractor = GetComponentInChildren<AreaInteractor>();
        }

    }

}
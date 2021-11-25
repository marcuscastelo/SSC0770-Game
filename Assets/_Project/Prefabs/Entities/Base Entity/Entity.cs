using UnityEngine;

using Hypnos.Entities.Systems;
using Hypnos.Entities.Components;

namespace Hypnos.Entities
{
    [ExecuteInEditMode]
    public class Entity : MonoBehaviour
    {
        [Header("Entity Components")]
        [SerializeField] private Health entityHealth;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;

        [Header("Entity Systems")]
        [SerializeField] private EntityController entityController;
        [SerializeField] private EntityMovement entityMovement;
        [SerializeField] private EntityCombat entityCombat;
        [SerializeField] private AreaInteractor entityInteractor;

        public Health Health => entityHealth;

        public EntityController Controller => entityController;
        public EntityMovement Movement => entityMovement;
        public EntityCombat Combat => entityCombat;
        public AreaInteractor Interactor => entityInteractor;

        [ContextMenu("Update Refs")]
        public void Awake()
        {
            entityHealth = GetComponentInChildren<Health>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponentInChildren<Animator>();

            entityController = GetComponentInChildren<EntityController>();
            entityMovement = GetComponentInChildren<EntityMovement>();
            entityCombat = GetComponentInChildren<EntityCombat>();
            entityInteractor = GetComponentInChildren<AreaInteractor>();
        }

    }

}
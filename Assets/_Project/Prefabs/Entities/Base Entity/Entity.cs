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
            entityHealth = GetComponent<Health>();

            entityController = GetComponentInChildren<EntityController>();
            entityMovement = GetComponentInChildren<EntityMovement>();
            entityCombat = GetComponentInChildren<EntityCombat>();
            entityInteractor = GetComponentInChildren<AreaInteractor>();
        }

    }

}
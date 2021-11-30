using UnityEngine;
using UnityEngine.SceneManagement;

using Hypnos.Entities.Systems;
using Hypnos.Entities.Components;

using Zenject;
using Hypnos.Core;

namespace Hypnos.Entities
{
    [ExecuteInEditMode]
    public class Entity : MonoBehaviour, IBuffable//, //TODO: IAttackable, ...
    {
        [SerializeField] private EntityStats _stats;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private HealthComponent _health;
        private IBuffable _buff;
        private EntityController _entityController;
        private IMoveable _entityMovement;
        private IAttacker _entityAttacker;
        private IAttackable _entityAttackable;
        private IInteractor _entityInteractor;
        private IEntityAudio<Entity> _entityAudio;

        [Inject]
        public void Construct(SpriteRenderer spriteRenderer, Animator animator, HealthComponent health, IBuffable buff, EntityController entityController, IMoveable entityMovement, IAttacker entityAttacker, IAttackable entityAttackable, IInteractor entityInteractor, IEntityAudio<Entity> entityAudioPlayer)
        {
            _spriteRenderer = spriteRenderer;
            _animator = animator;
            _health = health;
            _buff = buff;
            _entityController = entityController;
            _entityMovement = entityMovement;
            _entityAttacker = entityAttacker;
            _entityAttackable = entityAttackable;
            _entityInteractor = entityInteractor;
            _entityAudio = entityAudioPlayer;

            _buff.OnBuffAddedEvent += (addedBuff) =>
            {
                if (addedBuff.HasFlag(Buff.Defense))
                {
                    Health.SetMaxHealth(Health.MaxHealth + 1);
                    Health.SetHealth(Health.CurrentHealth + 1);
                }
            };

            _buff.OnBuffRemovedEvent += (removedBuff) =>
            {
                if (removedBuff.HasFlag(Buff.Defense))
                {
                    Health.SetMaxHealth(Health.MaxHealth - 1);
                }
            };

            _buff.ApplyBuff(_stats.initialBuff);

            //! TODO: remove this and inject on health component
            _health.SetMaxHealth(_stats.combatStats.maxHealth);
            _health.SetHealth(_stats.combatStats.maxHealth);
            _health.OnDeath += () =>
            {
                _entityMovement.Teleport(Vector2.one * -1000000);
                if (gameObject.name == "Player")
                {
                    Time.timeScale = 0;
                    DialogInfo dialogInfo = ScriptableObject.CreateInstance<DialogInfo>();
                    dialogInfo.title = "You died!";
                    dialogInfo.content = "You died!\nRestart?";
                    dialogInfo.buttons = DialogButtonCombination.OK;
                    DialogSystem.ShowDialog(new Dialog(dialogInfo, (result) =>
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        Time.timeScale = 1;
                    }));
                } //TODO death coroutine (allow sound to be played)
            };

            _health.OnHealthChanged += (newHealth) =>
            {
                Debug.Log("Health changed: " + newHealth);
                _entityAudio.PlayAttackSound(this);
            };
            //!<
        }

        // Facade - IBuffable
        public Buff ActiveBuff => _buff.ActiveBuff;
        public void ApplyBuff(Buff buff) => _buff.ApplyBuff(buff);
        public void RemoveBuff(Buff buff) => _buff.RemoveBuff(buff);
        public void SetBuff(Buff buff) => _buff.SetBuff(buff);
        public bool HasBuff(Buff buff) => _buff.HasBuff(buff);
        public void ClearBuffs() => _buff.ClearBuffs();
        public event System.Action<Buff> OnBuffAddedEvent { add => _buff.OnBuffAddedEvent += value; remove => _buff.OnBuffAddedEvent -= value; }
        public event System.Action<Buff> OnBuffRemovedEvent { add => _buff.OnBuffRemovedEvent += value; remove => _buff.OnBuffRemovedEvent -= value; }

        // // // Facade - IAttackable
        // // public void OnHurt(int damage) => entityCombat.OnHurt(damage);

        public HealthComponent Health => _health;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Animator Animator => _animator;

        public EntityController Controller => _entityController;
        public IMoveable Movement => _entityMovement;
        public IAttacker AttackerSystem => _entityAttacker;
        public IAttackable AttackableSystem => _entityAttackable;
        public IInteractor Interactor => _entityInteractor;


        //! This is a temporary solution to get the EntityController to work.
        public EntityStats Stats => _stats;
        public CombatStats CombatStats => _stats.combatStats;
        public WalkStats WalkStats => _stats.walkStats;
        public DashStats DashStats => _stats.dashStats;
        //!<

        [Inject]
        public void Construct(HealthComponent health, IBuffable buff)
        {
            _health = health;
            _buff = buff;
        }

        [ContextMenu("Update Refs")]
        public void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _animator = GetComponentInChildren<Animator>();

            _entityController = GetComponentInChildren<EntityController>();
            _entityMovement = GetComponentInChildren<EntityMovement>();
            _entityAttacker = GetComponentInChildren<EntityCombat>();
            _entityInteractor = GetComponentInChildren<AreaInteractor>();
        }

    }

}
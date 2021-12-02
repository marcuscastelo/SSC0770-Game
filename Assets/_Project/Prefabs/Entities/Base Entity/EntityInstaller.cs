using UnityEngine;
using Zenject;

using Hypnos.Entities.Components;
using Hypnos.Core;
using Hypnos.Entities.Systems;
using Hypnos.Entities;

public class EntityInstaller : MonoInstaller
{
    [SerializeField] private AreaInteractor interactor;
    [SerializeField] private EntityCombat combat;
    [SerializeField] private EntityMovement movement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Entity entity;
    [SerializeField] private EntityController controller;

    public override void InstallBindings()
    {
        Container.Bind<IBuffable>().To<BuffComponent>().AsSingle();
        Container.Bind<HealthComponent>().AsSingle();

        Container.Bind<IInteractor>().FromInstance(interactor).AsSingle();
        Container.Bind<IAttacker>().FromInstance(combat).AsSingle();
        Container.Bind<IAttackable>().FromInstance(combat).AsSingle();
        Container.Bind<IMoveable>().FromInstance(movement).AsSingle();
        Container.Bind<Animator>().FromInstance(animator).AsSingle();
        Container.Bind<SpriteRenderer>().FromInstance(spriteRenderer).AsSingle();
        Container.Bind<Entity>().FromInstance(entity).AsSingle();
        Container.Bind<EntityController>().FromInstance(controller).AsSingle();


        //Mock for testing and prototyping
        Container.Bind<IEntityAudio<Entity>>().To<TestEntityAudio>().AsSingle();
    }
}
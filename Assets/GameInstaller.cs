using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Hypnos.Core;
using Hypnos.Audio;
using Hypnos.Entities;

public class GameInstaller : MonoInstaller
{
    // [SerializeField] private Entity playerEntity;
    [SerializeField] private float initialClockTime = 60f;

    public override void InstallBindings()
    {
        Container.Bind<LevelSwitcher>().FromComponentInHierarchy().AsSingle();
        // Container.Bind<Entity>().WithId("Player").FromInstance(_playerEntity).AsSingle().NonLazy();

        Container.Bind<PlayerControls>().AsSingle().NonLazy();
        Container.Bind<UIControls>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<Clock>().AsSingle().NonLazy();
        Container.Bind<float>().FromInstance(initialClockTime).WhenInjectedInto<Clock>();
    }
}

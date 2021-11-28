using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Hypnos.Core;
using Hypnos.Entities;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Entity _playerEntity;

    public override void InstallBindings()
    {
        Container.Bind<LevelSwitcher>().FromComponentInHierarchy().AsSingle();
        // Container.Bind<Entity>().WithId("Player").FromInstance(_playerEntity).AsSingle().NonLazy();
    }
}

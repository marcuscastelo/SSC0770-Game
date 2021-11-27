using UnityEngine;
using Zenject;

using Hypnos.Entities.Components;
using Hypnos.Core;

public class EntityInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IBuffable>().To<BuffComponent>().AsSingle();
        Container.Bind<HealthComponent>().AsSingle();
    }
}
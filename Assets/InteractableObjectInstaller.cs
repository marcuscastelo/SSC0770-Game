using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Core;
using Zenject;

public class InteractableObjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Container.Bind<IInteractionResponse>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InteractableObject>().FromComponentInHierarchy().AsSingle();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Hypnos.Core;

public class GameInstaller : MonoInstaller
{
    [SerializeField]

    public override void InstallBindings()
    {
        Container.Bind<LevelSwitcher>().FromComponentInHierarchy().AsSingle();       
    }
}

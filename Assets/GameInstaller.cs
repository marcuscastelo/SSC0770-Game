using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Hypnos.Core;
using Hypnos.Audio;
using Hypnos.Entities;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Entity playerEntity;
    [SerializeField] private AudioRegistry audioRegistry;
    [SerializeField] private AudioSource globalSoundTrackSource;
    [SerializeField] private AudioSource globalSFXSource;

    public override void InstallBindings()
    {
        Container.Bind<LevelSwitcher>().FromComponentInHierarchy().AsSingle();
        // Container.Bind<Entity>().WithId("Player").FromInstance(_playerEntity).AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<AudioSystem>().AsSingle().NonLazy();
        Container.Bind<AudioRegistry>().FromInstance(audioRegistry).AsSingle();

        Container.Bind<AudioSource>().WithId(GlobalAudioSourceType.SoundTrack).FromInstance(globalSoundTrackSource).AsCached();
        Container.Bind<AudioSource>().WithId(GlobalAudioSourceType.SFX).FromInstance(globalSFXSource).AsCached();
    }
}

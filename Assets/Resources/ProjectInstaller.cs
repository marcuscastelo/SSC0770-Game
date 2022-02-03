using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using Hypnos.Audio;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private AudioRegistry audioRegistry;
    [SerializeField] private AudioSource globalSoundTrackSource;
    [SerializeField] private AudioSource globalSFXSource;


    public override void InstallBindings()
    {
        Container.Bind<AudioSource>().WithId(GlobalAudioSourceType.SoundTrack).FromInstance(globalSoundTrackSource).AsCached();
        Container.Bind<AudioSource>().WithId(GlobalAudioSourceType.SFX).FromInstance(globalSFXSource).AsCached();

        Container.BindInterfacesAndSelfTo<AudioSystem>().AsSingle().NonLazy();
        Container.Bind<AudioRegistry>().FromInstance(audioRegistry).AsSingle();
    }
}

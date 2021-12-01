using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Audio;
using Zenject;

public class MainMenuSound : MonoBehaviour
{
    private AudioSystem _audioSystem;
    
    [Inject]
    public void Construct(AudioSystem audioSystem)
    {
        _audioSystem = audioSystem;
    }

    void Start()
    {
        _audioSystem.PlaySoundTrack(AudioType.ST_MainMenu);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Audio;
using Zenject;

public class AudioTester : MonoBehaviour
{
    [SerializeField] private AudioType audioType;

    // private AudioSource _audioSource;
    private AudioSystem _audioSystem;

    [Inject]
    public void Construct(AudioSystem audioSystem)
    {
        this._audioSystem = audioSystem;
        // _audioSource = GetComponent<AudioSource>();
    }

    public void Test()
    {
        _audioSystem.PlaySFX(audioType);
    }

}

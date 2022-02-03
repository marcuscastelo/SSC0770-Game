using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Hypnos.Audio;
using Zenject;

public class VolumeSliderCallback : MonoBehaviour
{
    [Inject]
    private AudioSystem _audioSystem;

    private Slider _slider;

    public void OnValueChanged(float value)
    {
        _audioSystem.SetVolume(value);
    }

    void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    void Start()
    {
        _slider.value = _audioSystem.Volume;
    }
}

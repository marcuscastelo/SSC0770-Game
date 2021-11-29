using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hypnos.Audio;
using Zenject;

public class AudioTester : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSystem audioSystem;

    [Inject]
    public void Construct(AudioSystem audioSystem)
    {
        this.audioSystem = audioSystem;
        audioSource = GetComponent<AudioSource>();
    }

    float totalTime = 0;
    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        if (totalTime > 1)
        {
            Debug.Log("Playing");
            totalTime = 0;
            audioSystem.PlayAudio(AudioType.SFX_Hypnos_Attack_Sword, audioSource);
        }
    }
}

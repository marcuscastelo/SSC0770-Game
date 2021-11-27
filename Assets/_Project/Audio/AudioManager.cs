using UnityEngine;
using System.Collections.Generic;

namespace Hypnos.Audio
{
    public static class AudioManager
    {
        public static readonly List<AudioSource> audioSources = new List<AudioSource>();
        public static void RegisterAudioSource(AudioSource audioSource) => audioSources.Add(audioSource);
        public static void UnregisterAudioSource(AudioSource audioSource) => audioSources.Remove(audioSource);

        public static void Play(AudioClip clip)
        {
            foreach (var audioSource in audioSources)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                    return;
                }
            }
        }

    }
}
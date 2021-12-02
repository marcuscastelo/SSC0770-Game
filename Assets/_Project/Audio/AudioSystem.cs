using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

using Zenject;

namespace Hypnos.Audio
{
    public class AudioSystem : ITickable, IDisposable
    {
        private Dictionary<AudioType, IEnumerator> _jobTable = new Dictionary<AudioType, IEnumerator>();

        private AudioRegistry _audioRegistry;
        private AudioSource _globalSFXSource;
        private AudioSource _globalSoundTrackSource;

        private float _volume = 1.0f;
        public float Volume => _volume;

        [Inject]
        public AudioSystem(
            AudioRegistry audioRegistry,
            [Inject(Id=GlobalAudioSourceType.SFX)] AudioSource globalSFXSource,
            [Inject(Id=GlobalAudioSourceType.SoundTrack)] AudioSource globalSoundTrackSource)
        {
            _audioRegistry = audioRegistry;
            _globalSFXSource = globalSFXSource;
            _globalSoundTrackSource = globalSoundTrackSource;
        }

        //Play
        public void PlaySFX(AudioType type) => PlayAudio(type, _globalSFXSource);
        public void PlaySoundTrack(AudioType type) => PlayAudio(type, _globalSoundTrackSource);
        public void PlayAudio(AudioType type, AudioSource source)
        {
            ApplyVolumeToTransientAudio(source);
            if (!_audioRegistry.AudioTable.ContainsKey(type))
            {
                Debug.LogWarning($"[AudioSystem]: AudioType {type} not found in AudioRegistry");
                return;
            }

            AddJob(new AudioJob(AudioJob.AudioAction.Start, type, source));
        }

        //Stop
        public void StopSFX(AudioType type) => StopAudio(type, _globalSFXSource);
        public void StopSoundTrack(AudioType type) => StopAudio(type, _globalSoundTrackSource);
        public void StopAudio(AudioType type, AudioSource source)
        {
            AddJob(new AudioJob(AudioJob.AudioAction.Stop, type, source));
        }

        //Restart
        public void RestartSFX(AudioType type) => RestartAudio(type, _globalSFXSource);
        public void RestartSoundTrack(AudioType type) => RestartAudio(type, _globalSoundTrackSource);
        public void RestartAudio(AudioType type, AudioSource source)
        {
            ApplyVolumeToTransientAudio(source);
            AddJob(new AudioJob(AudioJob.AudioAction.Restart, type, source));
        }

        private void AddJob(AudioJob job)
        {
            RemoveConflictingJobs(job.audioType);

            IEnumerator jobRunner = RunJob(job);
            _jobTable.Add(job.audioType, jobRunner);
        }

        private void RemoveConflictingJobs(AudioType type)
        {
            if (_jobTable.ContainsKey(type))
            {
                RemoveJob(type);
            }

            //TODO: audio tracks conflict (global audio)
        }

        private IEnumerator RunJob(AudioJob job)
        {
            AudioSource source = job.audioSource;
            AudioObject audioObject = _audioRegistry.AudioTable[job.audioType];
            source.clip = audioObject.clip;

            switch (job.action)
            {
                case AudioJob.AudioAction.Start:
                    source.Play();
                    break;
                case AudioJob.AudioAction.Stop:
                    source.Stop();
                    break;
                case AudioJob.AudioAction.Restart:
                    source.Stop();
                    source.Play();
                    break;
            }

            _jobTable.Remove(job.audioType);
            yield return null;
        }

        private void RemoveJob(AudioType type)
        {
            if (!_jobTable.ContainsKey(type))
            {
                Debug.LogWarning("AudioSystem: Trying to remove job that doesn't exist");
                return;
            }

            IEnumerator job = _jobTable[type];
            //StopCoroutine(job); // Unneeded, because this is not a monobehavior
            _jobTable.Remove(type);
        }

        public void Tick()
        {
            Debug.Log("AudioSystem: Tick");

            //Makes a copy of the dictionary, because the enumerator will be modified
            foreach (KeyValuePair<AudioType, IEnumerator> job in new Dictionary<AudioType, IEnumerator>(_jobTable))
            {
                job.Value.MoveNext();
            }
        }

        public void Dispose()
        {
            // * If this was a monobehavior, we would need to stop all coroutines
            // foreach (IEnumerator job in _jobTable.Values)
            // {
            //     StopCoroutine(job);
            // }
            _jobTable.Clear();
        }


        public void PauseAllGlobals()
        {
            _globalSFXSource.Pause();
            _globalSoundTrackSource.Pause();
        }

        public void UnpauseAllGlobals()
        {
            _globalSFXSource.UnPause();
            _globalSoundTrackSource.UnPause();
        }

        public void SetVolume(float volume) {
            _volume = volume;
            _globalSFXSource.volume = _volume;
            _globalSoundTrackSource.volume = _volume;
        }

        private void ApplyVolumeToTransientAudio(AudioSource source)
        {
            source.volume = _volume;
        }
    }
}
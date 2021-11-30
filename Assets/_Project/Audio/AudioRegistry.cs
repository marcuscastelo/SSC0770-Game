using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypnos.Audio
{
    [CreateAssetMenu(fileName = "AudioRegistry", menuName = "Hypnos/Audio/AudioRegistry")]
    public class AudioRegistry : ScriptableObject
    {
        [SerializeField] private AudioObject[] _audios;

        private Dictionary<AudioType, AudioObject> _audioTable;
        public IReadOnlyDictionary<AudioType, AudioObject> AudioTable => _audioTable;

        public void OnEnable() => PopulateAudioTable();
        public void OnValidate() => PopulateAudioTable();

        private void PopulateAudioTable()
        {
            _audioTable = new Dictionary<AudioType, AudioObject>();
            foreach (var audio in _audios)
            {
                if (audio.type == AudioType.None) continue;

                if (audio == null)
                {
                    Debug.LogWarning($"AudioRegistry: AudioType {audio.type} is not assigned");
                    continue;
                }

                if (_audioTable.ContainsKey(audio.type))
                {
                    Debug.LogError($"AudioRegistry: Duplicate AudioType {audio.type}");
                    continue;
                }

                _audioTable.Add(audio.type, audio);
            }
        }
    }
}
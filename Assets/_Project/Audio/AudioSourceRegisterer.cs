using UnityEngine;
using UnityEngine.Assertions;

namespace Hypnos.Audio
{
    [ExecuteInEditMode]
    public class AudioSourceRegisterer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
            Assert.IsNotNull(audioSource);
        }

        private void OnEnable()
        {
            AudioSystem.RegisterAudioSource(audioSource);
        }

        private void OnDisable()
        {
            AudioSystem.UnregisterAudioSource(audioSource);
        }
    }
}
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
            AudioManager.RegisterAudioSource(audioSource);
        }

        private void OnDisable()
        {
            AudioManager.UnregisterAudioSource(audioSource);
        }
    }
}
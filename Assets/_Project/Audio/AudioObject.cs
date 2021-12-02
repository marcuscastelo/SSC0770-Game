using System;
using UnityEngine;

namespace Hypnos.Audio
{
    [Serializable]
    public class AudioObject
    {
        public AudioType type = AudioType.None;
        public AudioClip clip;
    }
}
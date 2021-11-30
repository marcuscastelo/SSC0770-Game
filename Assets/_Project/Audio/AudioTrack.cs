using UnityEngine;
using System;

namespace Hypnos.Audio
{

    [Serializable]
    public class AudioTrack
    {
        public AudioSource type;
        public AudioObject[] audioObjects;
    }

}
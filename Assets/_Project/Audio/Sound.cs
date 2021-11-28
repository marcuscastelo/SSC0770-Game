using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Audio/Sound", order = 1)]
public class Sound : ScriptableObject
{
    public AudioClip clip;
    public bool loop;
    public float volume;
    public float pitch;
}

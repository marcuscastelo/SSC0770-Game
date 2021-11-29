using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioJob
{
    public enum AudioAction { Start, Stop, Pause, Unpause, Restart }
    public readonly AudioAction action;
    public readonly AudioType audioType;
    public readonly AudioSource audioSource;

    public AudioJob(AudioAction action, AudioType audioType, AudioSource audioSource)
    {
        this.action = action;
        this.audioType = audioType;
        this.audioSource = audioSource;
    }
}

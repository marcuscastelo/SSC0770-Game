using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

using Zenject;

[Serializable]
public class Clock : ITickable
{
    private float currentTime, initialTime;
    public float CurrentTime => currentTime;
    public float InitialTime => initialTime;
    public bool Paused { get; set; }

    public event Action<float> OnTimeout = delegate { };

    public Clock(float time)
    {
        initialTime = currentTime = time;
    }

    public void Tick() => Tick(Time.deltaTime);
    public void Tick(float deltaTime)
    {
        if (!Paused) Decrement(deltaTime);
    }

    public void Decrement(float time)
    {
        currentTime = Mathf.Max(currentTime - time, 0);
    }

    public void Reset() => currentTime = initialTime;

    public void SetTime(float time) => Mathf.Ceil(currentTime = time);
}

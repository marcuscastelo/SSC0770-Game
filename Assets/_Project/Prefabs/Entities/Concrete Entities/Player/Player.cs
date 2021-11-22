using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent<Buff> onBuffChanged;

    [SerializeField]
    private Buff activeBuff;

    public Buff ActiveBuff
    {
        get
        {
            return activeBuff;
        }
        set
        {
            activeBuff = value;
            onBuffChanged.Invoke(activeBuff);
        }
    }

    // Editor code to make sure the stats are set properly
    private void OnValidate()
    {
        if (onBuffChanged != null)
            onBuffChanged.Invoke(activeBuff);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    public PlayerStats stats;

    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private Vector2 velocity;

    [SerializeField]
    private Buff activeBuff;

    public UnityEvent<Buff> onBuffChanged;

    private void Awake()
    {
        currentHealth = stats.maxHealth;
    }

    // Editor code to make sure the stats are set properly
    private void OnValidate()
    {
        if (stats == null)
            Debug.LogError("Player stats not set");

        if (onBuffChanged != null)
            onBuffChanged.Invoke(activeBuff);
    }
}
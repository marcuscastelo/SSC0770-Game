using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("References")]

    [SerializeField]
    public PlayerStats stats;

    public PlayerDisplay playerDisplay;
    
    public PlayerBrain brain;

    [Header("Player Status")]

    [SerializeField]
    private float currentHealth;

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

    public SelectableObject SelectedObject { get; set; }

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
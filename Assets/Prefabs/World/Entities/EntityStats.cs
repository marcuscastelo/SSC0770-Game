using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "EntityStats", menuName = "ScriptableObjects/EntityStats")]
public class EntityStats : ScriptableObject
{
    [Space(10)]
    [Header("Combat")]
    public int maxHealth;
    public int damage;


    [Space(10)]
    [Header("Movement")]
    public int acceleration;
    public int maxSpeed;
    [Range(0,1)] 
    public float friction;
}
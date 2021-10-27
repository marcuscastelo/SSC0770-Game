using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : ScriptableObject
{   
    [Header("Movement Stats")]
    [Range(0,70)]
    public float acceleration = 1;
    [Range(0,20)]
    public float maxSpeed = 20;
    [Range(0,1)]
    public float friction = 0.9f;

    [Space(10)]

    [Header("Combat Stats")]
    [Range(0, 5)]
    public int maxHealth = 1;
    [Range(0, 5)]
    public int attackDamage = 1;
    //DEF, SPEED, ETC...
}
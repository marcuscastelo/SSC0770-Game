using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Hypnos/Entity/Stats")]
public class EntityStats : ScriptableObject
{
   public CombatStats combatStats;
   public WalkStats walkStats;
   public DashStats dashStats;
}

[Serializable]
public class CombatStats
{
    public float attackDamage;
    public float attackCooldown;
    public float attackDuration;
    public float attackAnimatorMultiplier = 0.5f;
}

[Serializable]
public class WalkStats
{
    public float acceleration;
    public float maxSpeed;
    public float deceleration;
}

[Serializable]
public class DashStats
{
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
}